# Donatekart campaign-analysis API

> **Scope:** This guide describes the code actually committed to this repository. It does not assert successful runtime execution; see the linked CI checks. **No geographic map or quantitative performance chart is presented** because the repository contains no verified location or benchmark data.

## Project overview
An ASP.NET Web API 2 and MVC demonstration that retrieves campaign data from an external Donatekart test API and exposes derived campaign lists.

**Tech stack:** C#, .NET Framework 4.8, ASP.NET Web API, Newtonsoft.Json, HttpClient; no local database identified.

## Feature and component inventory
| Source path | Actual responsibility |
|---|---|
| `Donatekart/Controllers/DonatekartController.cs` | Fetches external campaign data and filters/sorts it |
| `Donatekart/Models/Campaign.cs` | Campaign metadata: title, amounts, dates, location and counts |
| `Donatekart/App_Start/WebApiConfig.cs` | Routes api/{controller}/{action}/{id} |
| `Donatekart.sln` | Visual Studio solution |

## Architecture diagram
~~~mermaid
flowchart LR
  User[Developer or HTTP client] --> Entry[C# .NET Framework application]
  Entry --> Logic[Application logic]
  Logic --> Data[External Donatekart campaign test API]
~~~

## Processing sequence
~~~mermaid
sequenceDiagram
  participant U as User or client
  participant E as Application
  participant D as Donatekart test API
  U->>E: Start application or request operation
  E->>D: Read required data
  D-->>E: Return data
  E-->>U: Processed result
~~~

## Installation and setup (source-grounded)
1. Install Visual Studio with ASP.NET and web development and the .NET Framework 4.8 developer pack on Windows.
2. Open Donatekart.sln; restore NuGet packages, build and launch Donatekart using IIS Express.
3. Use the port displayed by IIS Express; try /api/Donatekart/GetTotalSort, /api/Donatekart/GetActiveCamp and /api/Donatekart/GetCloseCamp.

~~~mermaid
flowchart TD
  A[Clone repository] --> B[Install development prerequisites]
  B --> C[Open solution and restore dependencies]
  C --> D[Build and run locally]
  D --> E{Results as expected?}
  E -->|Yes| F[Exercise core behavior]
  E -->|No| G[Review environment and known limits]
  G --> B
~~~

## Usage and functionality
GET /api/Donatekart/GetTotalSort sorts campaigns by totalAmount descending; GetActiveCamp selects end dates from today through 30 days ahead; GetCloseCamp selects past-end or fully-procured campaigns.

## Testing and verification
- The added GitHub Actions workflow validates local Markdown links and fenced blocks and attempts a Windows .NET Framework solution build.
- Building the solution is **not equivalent** to running the application or checking external services.
- Check the PR's **Checks** tab; do not consider this validated until CI passes.
- Manual verification: follow the setup above and compare outcomes with the source and sample files.

## Known limitations and review recommendations
- The external testapi.donatekart.com/api/campaign endpoint may be unavailable; do not imply live connectivity.
- The controller initializes a static campaign list by blocking on an async request; this is not live-updated per request and startup errors may propagate.
- No authentication, caching policy, local persistence or end-to-end tests are demonstrated.
- Do not publish credentials, tokens or developer-specific data from configuration into documentation.
- This documentation-only PR does not change core application behavior.

## Next steps
Run the application in the documented environment, add dedicated automated tests around core behavior, and separate any necessary code fixes into subsequent PRs.
