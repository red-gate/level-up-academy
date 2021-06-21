function Get-Planets {
    param (
        [string]$Name = '*'
    )
    
    begin {
        Write-Host "Listing planets matching $Name"
    }
    
    process {
        $planets = @(
            @{ Name = 'Mercury' }
            @{ Name = 'Venus' }
            @{ Name = 'Earth' }
            @{ Name = 'Mars' }
            @{ Name = 'Jupiter' }
            @{ Name = 'Saturn' }
            @{ Name = 'Uranus' }
            @{ Name = 'Neptune' }
        ) | ForEach-Object { [PSCustomObject] $_ }
    
        $planets | Where-Object { $_.Name -like $Name }
            
    }
    
    end {
        Write-Host "(End of list)"
    }
}