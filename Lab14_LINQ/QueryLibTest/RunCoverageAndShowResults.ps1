# Ключ про-версии добавлять через system environents
# Команда: setx REPORTGENERATOR_PRO_VERSION_KEY "KEY"
# После выполнения команды перезапустить Visual Studio
# Без введенного ключа будет сгенерирован базовый отчет без дополнительной информации
$proVersionKey = $env:REPORTGENERATOR_PRO_VERSION_KEY

# Запуск тестов
dotnet test --collect:"XPlat Code Coverage"

# Поиск coverage.cobertura.xml
$coverageFile = Get-ChildItem TestResults -Recurse -Filter coverage.cobertura.xml

# Папка для отчета
$reportDir = ".\CoverageReport"

# Генерация отчеа с Pro‑ключом
reportgenerator `
    -license:$proVersionKey `
    -reports:$coverageFile.FullName `
    -targetdir:$reportDir `
    -reporttypes:"Html" `
    -assemblyfilters:"+QueryLib;-OrganizationLib"

# Открытие отчета
Start-Process (Join-Path $reportDir "index.html")