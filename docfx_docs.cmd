docfx build docfx/docfx.json
rmdir docs /s /q
xcopy docfx\_site\* docs\ /e /y
