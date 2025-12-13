# Copilot instructions for TravelToursConsole3

This file gives concise, repo-specific guidance for an AI coding agent to be immediately productive.

**Project Overview:**
- **Type:** Console application (C# .NET 9) with a simple menu in `Program.cs`.
- **Main components:** `Program.cs` (root menu), `Services/` (business logic), `Interface/` (interfaces).
- **Example:** `Registration_Login_Service` implements `IRandL` and is invoked by the menu option in `Program.cs`.

**Build & Run**
- Build the solution from the repo root:
  - `dotnet build TravelTourConsole.sln`
- Run the console app from the project folder:
  - `cd TravelTourConsole`
  - `dotnet run`
- The project targets `net9.0` (see `TravelTourConsole/TravelTourConsole.csproj`).

**Key files to inspect**
- `Program.cs` — top-level menu and user input handling. Add menu options here.
- `Services/Registration_Login_Service.cs` — example of a service class that handles registration/login and uses `Console` for I/O.
- `Interface/IRandL.cs` — interface defining `login` and `Registration` used by the service.

**Repo-specific conventions & patterns**
- Folder layout: `Interface/` holds interfaces, `Services/` holds service implementations. Follow this separation when adding new behavior.
- Naming and casing: this repo uses some non-standard names (e.g., class `Registration_Login_Service`, interface `IRandL`, method `login` with lowercase). Preserve existing names when editing to avoid breaking references.
- Console-driven UI: user interaction is handled synchronously via `Console.ReadLine`, `Console.Clear`, and `Console.ReadKey`. When adding features, ensure the menu flow and console color changes (e.g., `Console.ForegroundColor`) remain consistent.
- No external dependencies: the project currently has no NuGet packages—changes should not assume third-party libraries unless you add them and update the `.csproj`.

**Common edits and patterns**
- To add a new menu item:
  1. Modify `Program.cs` menu text and add a new branch in the main loop.
  2. Create a new service class in `Services/`, or extend an existing service.
  3. If behavior should be testable or pluggable, add an interface to `Interface/` and implement it in `Services/`.
- To add a new service that fits current style, follow the `Registration_Login_Service` pattern: simple public class, synchronous console I/O, and minimal state.

**Debugging & quick checks**
- Run `dotnet build` to catch compile issues. Use `dotnet run` for quick manual verification of console flows.
- If you change method or class names, search the repo for usages (e.g., `IRandL`, `Registration_Login_Service`) before committing.

**What to watch for (gotchas)**
- Method and identifier casing is inconsistent (e.g., `login` vs `Registration`). Renaming is allowed but update all references and preserve expected behavior.
- Console UI is the primary UX; ensure new features don't deadlock waiting for input.
- There are no tests or CI configured—add tests via a new `*.Tests` project and add it to `TravelTourConsole.sln` if you introduce non-trivial logic.

**If you need to extend the project**
- Prefer small, incremental PRs: update `Program.cs` + one service at a time.
- Add an interface in `Interface/` for logic that should be mocked/tested. Keep console I/O in a thin layer so core logic can be unit-tested.

If anything above is unclear or you want a different level of guidance (more examples, test scaffolding, or CI config), tell me what to add.
