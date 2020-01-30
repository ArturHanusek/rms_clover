program uninstallHooks;

{$APPTYPE CONSOLE}

uses
  SysUtils,
  uApp in 'class\uApp.pas';

begin

  APP := TAPP.Create;

  try
    { TODO -oUser -cConsole Main : Insert code here }

    APP.InstallHooks;

  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);

  end;

  APP.Free;

end.
