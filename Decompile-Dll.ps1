<#
.SYNOPSIS
    Decompiles a .NET DLL (or EXE) into a full C# Visual Studio project.

.DESCRIPTION
    Uses ilspycmd (the CLI for the ICSharpCode.Decompiler engine — the same
    engine dnSpy and ILSpy use under the hood) to export a complete project
    structure: .csproj file, folders per namespace, and .cs files per type.

.USAGE
    .\Decompile-Dll.ps1 -DllPath "C:\path\to\SomeAssembly.dll"
    .\Decompile-Dll.ps1 -DllPath "C:\path\to\SomeAssembly.dll" -OutputFolder "C:\Decompiled\SomeAssembly"
    .\Decompile-Dll.ps1 -DllPath "C:\path\to\SomeAssembly.dll" -OpenInVS

.NOTES
    Requires the .NET SDK (not just the runtime) to be installed, since
    ilspycmd is installed as a dotnet global tool.
#>

param(
    [Parameter(Mandatory = $true)]
    [string]$DllPath,

    [string]$OutputFolder,

    [switch]$OpenInVS
)

# --- Validate input ---------------------------------------------------
if (-not (Test-Path $DllPath)) {
    Write-Error "File not found: $DllPath"
    exit 1
}

$DllPath = (Resolve-Path $DllPath).Path
$dllName = [System.IO.Path]::GetFileNameWithoutExtension($DllPath)

if (-not $OutputFolder) {
    $OutputFolder = Join-Path (Split-Path $DllPath -Parent) "$dllName-decompiled"
}

Write-Host "DLL:    $DllPath"
Write-Host "Output: $OutputFolder"
Write-Host ""

# --- Check for .NET SDK -------------------------------------------------
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Error "dotnet CLI not found. Install the .NET SDK first: https://dotnet.microsoft.com/download"
    exit 1
}

# --- Check for ilspycmd, install if missing -----------------------------
$hasIlspy = $false
try {
    $toolList = dotnet tool list -g 2>$null
    if ($toolList -match "ilspycmd") { $hasIlspy = $true }
} catch { }

if (-not $hasIlspy) {
    Write-Host "ilspycmd not found — installing as a global dotnet tool..."
    dotnet tool install -g ilspycmd
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to install ilspycmd. See errors above."
        exit 1
    }
    # Make sure this session's PATH picks up the tools dir
    $toolsPath = Join-Path $env:USERPROFILE ".dotnet\tools"
    if ($env:PATH -notlike "*$toolsPath*") {
        $env:PATH = "$env:PATH;$toolsPath"
    }
}

# --- Create output folder ------------------------------------------------
New-Item -ItemType Directory -Path $OutputFolder -Force | Out-Null

# --- Run the decompiler ---------------------------------------------------
# -p           : generate a full project (.csproj + folders + .cs files)
# -o <folder>  : output directory
Write-Host "Decompiling..."
ilspycmd -p -o "$OutputFolder" "$DllPath"

if ($LASTEXITCODE -ne 0) {
    Write-Error "Decompilation failed. The DLL may be obfuscated or use unsupported IL features."
    exit 1
}

# --- Find the generated .csproj -------------------------------------------
$csproj = Get-ChildItem -Path $OutputFolder -Filter "*.csproj" -Recurse | Select-Object -First 1

Write-Host ""
Write-Host "Done. Project exported to:"
Write-Host "  $OutputFolder"

if ($csproj) {
    Write-Host "Project file:"
    Write-Host "  $($csproj.FullName)"

    if ($OpenInVS) {
        Write-Host "Opening in Visual Studio..."
        Start-Process $csproj.FullName
    }
} else {
    Write-Warning "No .csproj found in output — check the ilspycmd output above for errors."
}
