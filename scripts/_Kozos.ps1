# Közös függvények a szkriptekhez. Nem önálló futtatásra – dot-source-szal töltik be.
$ErrorActionPreference = 'Stop'
$Gyoker = Split-Path -Parent $PSScriptRoot
$Utf8NoBom = New-Object System.Text.UTF8Encoding($false)

function Read-Utf8([string]$Ut) { [IO.File]::ReadAllText($Ut, [Text.Encoding]::UTF8) }
function Write-Utf8([string]$Ut, [string]$Szoveg) { [IO.File]::WriteAllText($Ut, $Szoveg, $Utf8NoBom) }

function Get-Konfig {
    Read-Utf8 (Join-Path $Gyoker 'ecosystem.config.json') | ConvertFrom-Json
}

function Get-Evfolyamok {
    (Get-Konfig).evfolyamok.PSObject.Properties | ForEach-Object { $_.Name }
}

# A README.md elején lévő YAML-fejléc egyszerű (kulcs: érték) beolvasása.
function Get-Fejlec([string]$Ut) {
    $m = [regex]::Match((Read-Utf8 $Ut), '^﻿?---\r?\n(.*?)\r?\n---', 'Singleline')
    if (-not $m.Success) { return $null }
    $h = [ordered]@{}
    foreach ($sor in $m.Groups[1].Value -split '\r?\n') {
        if ($sor -match '^\s*([A-Za-z_]+)\s*:\s*(.*)$') { $h[$Matches[1]] = $Matches[2].Trim() }
    }
    $h
}

# Az első "# " kezdetű sor szövege (modul- és feladatcímekhez).
function Get-ElsoCim([string]$Ut) {
    if (-not (Test-Path $Ut)) { return $null }
    foreach ($sor in (Read-Utf8 $Ut) -split '\r?\n') {
        if ($sor -match '^#\s+(.+)$') { return $Matches[1].Trim() }
    }
    $null
}

function Get-RelUt([string]$Teljes) {
    $Teljes.Substring($Gyoker.Length).TrimStart('\', '/') -replace '\\', '/'
}

function Get-GitHubUrl([string]$RelUt) {
    $k = Get-Konfig
    $kodolt = ($RelUt -split '/' | ForEach-Object { [Uri]::EscapeDataString($_) }) -join '/'
    "$($k.githubRepo.TrimEnd('/'))/tree/$($k.branch)/$kodolt"
}

# Minden lecke: <évfolyam>/Mxx-*/Lxx-*/README.md, érvényes fejléccel.
function Get-Leckek {
    foreach ($evf in Get-Evfolyamok) {
        $evfMappa = Join-Path $Gyoker $evf
        if (-not (Test-Path $evfMappa)) { continue }
        foreach ($modul in Get-ChildItem $evfMappa -Directory | Where-Object Name -match '^M\d{2}-') {
            foreach ($lecke in Get-ChildItem $modul.FullName -Directory | Where-Object Name -match '^L\d{2}-') {
                $readme = Join-Path $lecke.FullName 'README.md'
                if (-not (Test-Path $readme)) { continue }
                $f = Get-Fejlec $readme
                if (-not $f -or -not $f.id) { Write-Warning "Hiányzó fejléc: $readme"; continue }
                [pscustomobject]@{
                    Id         = $f.id
                    Cim        = $f.cim
                    Evfolyam   = $evf
                    Modul      = $f.modul
                    ModulMappa = $modul.Name
                    Orak       = $f.orak
                    Het        = $f.het
                    Oraszam    = $f.oraszam
                    Leiras     = $f.leiras
                    Mappa      = $lecke.FullName
                    RelUt      = Get-RelUt $lecke.FullName
                }
            }
        }
    }
}

# Egy lecke Classroom-anyaga: téma, cím, leírás és a link-mellékletek.
# A Classroom-Poszt.ps1 (kézi) és a Classroom-Jegyzek.ps1 (automatikus feltöltés) is ezt használja.
function Get-ClassroomAnyag($l) {
    $k = Get-Konfig
    $leckeUrl = Get-GitHubUrl $l.RelUt
    $linkek = New-Object System.Collections.Generic.List[string]
    $linkek.Add($leckeUrl)

    $sorok = New-Object System.Collections.Generic.List[string]
    if ($l.Leiras) { $sorok.Add($l.Leiras); $sorok.Add('') }
    $sorok.Add("Tananyag (GitHub): $leckeUrl")
    $sorok.Add("Iskolai hálózaton: $($k.halozatiUtvonal.TrimEnd('\'))\$($l.RelUt -replace '/', '\')")

    $feladatok = @(Get-ChildItem (Join-Path $l.Mappa 'feladatok') -Directory -ErrorAction SilentlyContinue |
        Where-Object Name -match '^F\d{2}-' | Sort-Object Name)
    if ($feladatok) {
        $sorok.Add(''); $sorok.Add('Gyakorló feladatok:')
        foreach ($f in $feladatok) {
            $cim = Get-ElsoCim (Join-Path $f.FullName 'README.md')
            if (-not $cim) { $cim = $f.Name }
            $url = Get-GitHubUrl "$($l.RelUt)/feladatok/$($f.Name)"
            $sorok.Add("  - $cim  ->  $url")
            $linkek.Add($url)
        }
    }

    [pscustomobject]@{
        id     = $l.Id
        tema   = "$($l.Id.Substring(0, 3))$($l.Modul)"   # közös kurzus: pl. 13-M02 – Ciklusok
        cim    = "$($l.Id) · $($l.Cim)"
        leiras = $sorok -join "`n"
        linkek = @($linkek | Select-Object -First 20)    # a Classroom legfeljebb 20 mellékletet enged
    }
}
