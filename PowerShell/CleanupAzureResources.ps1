Connect-AzAccount -Tenant ""
Set-AzContext -Subscription ""
$rgs = Get-AzResourceGroup

foreach ($rg in $rgs) {
    
    if ($rg.ResourceGroupName -eq "abc") {
        Write-Output "Skipping abc"
        $rg.ResourceGroupName
    }
    else {
        Get-AzResourceGroup -Name $rg.ResourceGroupName | Remove-AzResourceGroup -Force
    }
}