<#
.SYNOPSIS
  Google Classroom-anyag szövegét állítja elő egy leckéhez (cím, téma, leírás, linkek),
  és a leírást a vágólapra másolja. Kézi feltöltéshez; az automatikushoz lásd Classroom-Jegyzek.ps1.
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
$a = Get-ClassroomAnyag $l
$leiras = $a.leiras -replace "`n", "`r`n"

Write-Host "TÉMA:  $($a.tema)"
Write-Host "CÍM:   $($a.cim)"
Write-Host "LEÍRÁS:"
Write-Host $leiras
if (-not $NemMasol) {
    Set-Clipboard -Value $leiras
    Write-Host "`n(A leírás a vágólapra másolva.)"
}
