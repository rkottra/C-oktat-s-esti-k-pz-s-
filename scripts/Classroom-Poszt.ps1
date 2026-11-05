<#
.SYNOPSIS
  Google Classroom-anyag szövegét állítja elő egy leckéhez (cím, téma, leírás, linkek),
  és a leírást a vágólapra másolja.
.EXAMPLE
  .\scripts\Classroom-Poszt.ps1 -Id 13-M02-L03
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][ValidatePattern('^\d{2}-M\d{2}-L\d{2}$')][string]$Id,
    [switch]$NemMasol
)
. "$PSScriptRoot\_Kozos.ps1"

$l = Get-Leckek | Where-Object Id -eq $Id
if (-not $l) { throw "Nincs ilyen lecke: $Id" }
$k = Get-Konfig

$sorok = New-Object System.Collections.Generic.List[string]
if ($l.Leiras) { $sorok.Add($l.Leiras); $sorok.Add('') }
$sorok.Add("Tananyag (GitHub): $(Get-GitHubUrl $l.RelUt)")
$sorok.Add("Iskolai hálózaton: $($k.halozatiUtvonal.TrimEnd('\'))\$($l.RelUt -replace '/', '\')")

$feladatMappa = Join-Path $l.Mappa 'feladatok'
$feladatok = @(Get-ChildItem $feladatMappa -Directory -ErrorAction SilentlyContinue | Where-Object Name -match '^F\d{2}-' | Sort-Object Name)
if ($feladatok) {
    $sorok.Add(''); $sorok.Add('Gyakorló feladatok:')
    foreach ($f in $feladatok) {
        $cim = Get-ElsoCim (Join-Path $f.FullName 'README.md')
        if (-not $cim) { $cim = $f.Name }
        $sorok.Add("  - $cim  ->  $(Get-GitHubUrl "$($l.RelUt)/feladatok/$($f.Name)")")
    }
}
$leiras = $sorok -join "`r`n"

Write-Host "TÉMA:  $($l.Id.Substring(0, 3))$($l.Modul)"   # közös kurzus: pl. 13-M02 – Ciklusok
Write-Host "CÍM:   $($l.Id) · $($l.Cim)"
Write-Host "LEÍRÁS:"
Write-Host $leiras
if (-not $NemMasol) {
    Set-Clipboard -Value $leiras
    Write-Host "`n(A leírás a vágólapra másolva.)"
}
Write-Host "`nKözzététel után írd be a Classroom-linket a lecke fejlécébe: classroom: <link>"
