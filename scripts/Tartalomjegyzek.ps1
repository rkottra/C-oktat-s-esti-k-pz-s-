<#
.SYNOPSIS
  Az évfolyam-README-k tartalomjegyzékét újragenerálja a leckék fejlécéből
  (a <!-- TARTALOM:START --> és <!-- TARTALOM:END --> jelölők között).
#>
[CmdletBinding()]
param()
. "$PSScriptRoot\_Kozos.ps1"

$START = '<!-- TARTALOM:START -->'
$END = '<!-- TARTALOM:END -->'
$leckek = @(Get-Leckek)

foreach ($evf in Get-Evfolyamok) {
    $readme = Join-Path $Gyoker "$evf\README.md"
    if (-not (Test-Path $readme)) { continue }

    $sb = New-Object System.Text.StringBuilder
    $sajat = @($leckek | Where-Object Evfolyam -eq $evf | Sort-Object Id)
    if (-not $sajat) {
        [void]$sb.AppendLine('*(még nincs lecke)*')
    }
    foreach ($csoport in $sajat | Group-Object ModulMappa) {
        $modulCim = Get-ElsoCim (Join-Path $Gyoker "$evf\$($csoport.Name)\README.md")
        if (-not $modulCim) { $modulCim = $csoport.Name }
        [void]$sb.AppendLine("### [$modulCim]($($csoport.Name)/)").AppendLine()
        [void]$sb.AppendLine('| ID | Lecke | Órák | Hét |').AppendLine('|---|---|---|---|')
        foreach ($l in $csoport.Group) {
            $rel = $l.RelUt.Substring($evf.Length + 1)
            [void]$sb.AppendLine("| $($l.Id) | [$($l.Cim)]($rel/) | $($l.Orak) | $($l.Het) |")
        }
        [void]$sb.AppendLine()
    }

    $sz = Read-Utf8 $readme
    $i = $sz.IndexOf($START); $j = $sz.IndexOf($END)
    if ($i -lt 0 -or $j -lt $i) { Write-Warning "Nincs TARTALOM-jelölő: $readme"; continue }
    $uj = $sz.Substring(0, $i + $START.Length) + "`n" + $sb.ToString().TrimEnd() + "`n" + $sz.Substring($j)
    if ($uj -ne $sz) { Write-Utf8 $readme $uj; Write-Host "Frissítve: $evf/README.md ($($sajat.Count) lecke)" }
    else { Write-Host "Nem változott: $evf/README.md" }
}
