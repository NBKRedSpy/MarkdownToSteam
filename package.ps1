$ErrorActionPreference = 'Stop'


md -Force -ErrorAction SilentlyContinue ./publish/targets/
md -Force -ErrorAction SilentlyContinue ./publish/packages/

function publishOs($target)
{
    Write-Output ("----- Building " + $target)
    dotnet publish /p:DebugType=None /p:DebugSymbols=false --configuration Release --runtime $target --sc -o ./publish/targets/$target /p:PublishSingleFile=true .\MarkdownToSteam\MarkdownToSteam.csproj
    Compress-Archive -Force -DestinationPath ("./publish/packages/" + "MarkdownToSteam-" + $target + ".zip") ./publish/targets/$target/*
}

publishOs "osx-arm64"
publishOs "win-x64"

# dir -Directory .\publish\targets | %{Compress-Archive -Force -DestinationPath ("./publish/packages/" + $_.Name + ".zip") $_/* }    
