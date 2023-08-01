$ErrorActionPreference = 'Stop'


md -Force -ErrorAction SilentlyContinue ./publish/targets/ > $null
md -Force -ErrorAction SilentlyContinue ./publish/packages/ > $null


# Requires a clean or the partial trimming will sometimes give trimming issues.
dotnet clean -c Release
dotnet build -c Release

function publishOs($target)
{
    Write-Output ("----- Building " + $target)
    dotnet publish /p:TrimeMode=partial /p:DebugType=None /p:DebugSymbols=false --configuration Release --runtime $target --sc -o ./publish/targets/$target /p:PublishTrimmed=true /p:PublishSingleFile=true .\MarkdownToSteam\MarkdownToSteam.csproj
    Compress-Archive -Force -DestinationPath ("./publish/packages/" + "MarkdownToSteam-" + $target + ".zip") ./publish/targets/$target/*
}

publishOs "osx-arm64"
publishOs "win-x64"

"------------  Files"

dir -Recurse *.zip | %{write-host ($_.FullName + " ") -NoNewline }
Write-Host
