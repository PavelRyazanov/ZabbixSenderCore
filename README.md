# ZabbixSenderCore

Lightweight C# zabbix client


# Installing

On dotnet CLI call: `dotnet add package ZabbixSenderCore`

On Package Manager call: `Install-Package ZabbixSenderCore`


# Configuration

Configuration in `appsettings.json` file

```
{
    "Zabbix": {
        "ServerAddress": "https://zabbix_server",
        "ServerPort": 10051,
        "ConnectionTimeout": 150
    }
}
```