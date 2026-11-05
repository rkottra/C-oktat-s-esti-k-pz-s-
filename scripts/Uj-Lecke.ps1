<#
.SYNOPSIS
  Új lecke létrehozása a _sablon/lecke alapján.
.EXAMPLE
  .\scripts\Uj-Lecke.ps1 -Evfolyam 1-13 -Modul M02-ciklusok -ModulCim "Ciklusok" -Lecke L03-while-ciklus -Cim "A while ciklus"
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][ValidateSet('1-13', '2-14')][string]$Evfolyam,
    [Parameter(Mandatory)][ValidatePattern('^M\d{2}-[a-z0-9-]+$')][string]$Modul,
    [Parameter(Mandatory)][ValidatePattern('^L\d{2}-[a-z0-9-]+$')][string]$Lecke,
    [Parameter(Mandatory)][string]$Cim,
    # Csak új modulnál kell: a modul olvasható neve (pl. "Ciklusok").
    [string]$ModulCim
)
. "$PSScriptRoot\_Kozos.ps1"

$modulKod = $Modul.Substring(0, 3)
$id = '{0}-{1}-{2}' -f $Evfolyam.Split('-')[1], $modulKod, $Lecke.Substring(0, 3)

if (Get-Leckek | Where-Object Id -eq $id) { throw "A(z) $id azonosító már foglalt." }

$modulMappa = Join-Path $Gyoker "$Evfolyam\$Modul"
$modulReadme = Join-Path $modulMappa 'README.md'
if (-not (Test-Path $modulMappa)) {
    if (-not $ModulCim) { throw "Új modul ($Modul): add meg a -ModulCim paramétert is." }
    New-Item -ItemType Directory $modulMappa | Out-Null
    Write-Utf8 $modulReadme "# $modulKod – $ModulCim`n`n## Leckék`n`nLásd: [évfolyam-tartalomjegyzék](../README.md)`n"
    Write-Host "Új modul: $Evfolyam/$Modul"
}
$modulNev = Get-ElsoCim $modulReadme
if (-not $modulNev) { $modulNev = "$modulKod – $ModulCim" }

$cel = Join-Path $modulMappa $Lecke
Copy-Item (Join-Path $Gyoker '_sablon\lecke') $cel -Recurse

Get-ChildItem $cel -Recurse -Filter *.md | ForEach-Object {
    $sz = (Read-Utf8 $_.FullName).
        Replace('{{ID}}', $id).Replace('{{CIM}}', $Cim).
        Replace('{{EVFOLYAM}}', $Evfolyam).Replace('{{MODUL}}', $modulNev)
    Write-Utf8 $_.FullName $sz
}

Write-Host "Kész: $id  ->  $(Get-RelUt $cel)"
Write-Host "Következő: töltsd ki a fejlécet (het, oraszam, leiras), majd futtasd a Tartalomjegyzek.ps1-et."
