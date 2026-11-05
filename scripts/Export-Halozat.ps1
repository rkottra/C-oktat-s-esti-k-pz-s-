<#
.SYNOPSIS
  Elkészíti a hálózati mappába szánt exportot a (gitből kizárt) _halozat/ mappába.
  Ugyanaz a szerkezet, mint a GitHubon; minden leckébe _GitHub.url
  parancsikon kerül (Classroom-link szándékosan nem). A _halozat/ tükrözött: ami a repóból törlődött, onnan is törlődik.
  A hálózati mappába juttatás a saját szinkronodra van bízva.
#>
[CmdletBinding()]
param()
. "$PSScriptRoot\_Kozos.ps1"

$k = Get-Konfig
$cel = if ([IO.Path]::IsPathRooted($k.halozatExportMappa)) { $k.halozatExportMappa } else { Join-Path $Gyoker $k.halozatExportMappa }

# Figyelmeztetés, ha nem kiadott (nem commitolt) változás is kimenne.
$commit = '?'
if (Get-Command git -ErrorAction SilentlyContinue) {
    Push-Location $Gyoker
    $eap = $ErrorActionPreference; $ErrorActionPreference = 'Continue'   # PS 5.1: a git stderr-je ne legyen végzetes
    try {
        $commit = git rev-parse --short HEAD 2>$null
        $valtozas = git status --porcelain 2>$null
    } finally { $ErrorActionPreference = $eap; Pop-Location }
    if (-not $commit) { $commit = '(még nincs commit)' }
    if ($valtozas) { Write-Warning 'Van nem commitolt változás – az is bekerül az exportba.' }
}

# Név szerint bárhol kizárva: .vs, bin, obj; a többi csak a repó gyökerében.
$kizartMappak = @('.vs', 'bin', 'obj') +
    (@('.git', '.github', 'docs', 'scripts', '_sablon', '_bemenet') | ForEach-Object { Join-Path $Gyoker $_ }) +
    @($cel)
$kizartFajlok = @('.gitignore', '.gitattributes', '.gitkeep', 'ecosystem.config.json', 'ecosystem.local.json', '*.user', '*.suo',
                  '_GitHub.url', '_Classroom.url', '_FRISSITVE.txt')

$robocopyArgs = @($Gyoker, $cel, '/MIR', '/R:1', '/W:1', '/NFL', '/NDL', '/NJH', '/NP',
                  '/XD') + $kizartMappak + @('/XF') + $kizartFajlok
& robocopy @robocopyArgs | Out-Null
if ($LASTEXITCODE -ge 8) { throw "A robocopy hibával állt le (kód: $LASTEXITCODE)." }

function New-UrlFajl([string]$Ut, [string]$Url) {
    Write-Utf8 $Ut "[InternetShortcut]`r`nURL=$Url`r`n"
}

New-UrlFajl (Join-Path $cel '_GitHub.url') $k.githubRepo
foreach ($evf in Get-Evfolyamok) {
    $evfCel = Join-Path $cel $evf
    if (-not (Test-Path $evfCel)) { continue }
    New-UrlFajl (Join-Path $evfCel '_GitHub.url') (Get-GitHubUrl $evf)
}

$db = 0
foreach ($l in Get-Leckek) {
    $lCel = Join-Path $cel ($l.RelUt -replace '/', '\')
    New-UrlFajl (Join-Path $lCel '_GitHub.url') (Get-GitHubUrl $l.RelUt)
    $db++
}

Write-Utf8 (Join-Path $cel '_FRISSITVE.txt') (@(
    'Ez a mappa a GitHub-repóból generált másolat – itt NE szerkessz, a változtatás a GitHubon történjen.'
    ''
    "Forrás:     $($k.githubRepo)"
    "Commit:     $commit"
    "Generálva:  $(Get-Date -Format 'yyyy-MM-dd HH:mm')"
) -join "`r`n")

Write-Host "Export kész: $cel  ($db lecke, commit: $commit)"
exit 0   # a robocopy 1–7-es "siker" kódja ne látsszon hibának
