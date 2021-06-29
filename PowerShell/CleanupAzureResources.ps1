# Delete all the azure resource groups from the given sub. Honors the exeption resource group.
Connect-AzAccount -Tenant ""
Set-AzContext -Subscription ""
$exceptionRg ="azloadalpha-rg"

$rgs = Get-AzResourceGroup

foreach ($rg in $rgs) {
    
    if ($rg.ResourceGroupName -eq $exceptionRg) {
        Write-Output "Skipping"
        $rg.ResourceGroupName
    }
    else {
        Get-AzResourceGroup -Name $rg.ResourceGroupName | Remove-AzResourceGroup -Force
    }
}