<#
.SYNOPSIS
  Előállítja a scripts/classroom/anyagok.json jegyzéket: minden lecke Classroom-anyaga
  (téma, cím, leírás, linkek) tanmenet szerinti sorrendben. A jegyzéket a Google Apps Script
  (scripts/classroom/Kod.gs) tölti le a GitHubról, és ez alapján frissíti a kurzust.
  A jegyzék Classroom-azonosítót NEM tartalmaz, ezért mehet a nyilvános repóba.
.EXAMPLE
  .\scripts\Classroom-Jegyzek.ps1
#>
[CmdletBinding()]
param()
. "$PSScriptRoot\_Kozos.ps1"

$anyagok = @(Get-Leckek | Sort-Object Id | ForEach-Object { Get-ClassroomAnyag $_ })
$jegyzek = [ordered]@{
    leiras  = 'Generált fájl (scripts/Classroom-Jegyzek.ps1) – kézzel ne szerkeszd.'
    anyagok = $anyagok
}
$cel = Join-Path $Gyoker 'scripts\classroom\anyagok.json'
Write-Utf8 $cel (($jegyzek | ConvertTo-Json -Depth 5) + "`n")
Write-Host "$($anyagok.Count) anyag, $(@($anyagok.tema | Sort-Object -Unique).Count) téma -> $(Get-RelUt $cel)"
