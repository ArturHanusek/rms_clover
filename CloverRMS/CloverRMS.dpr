library CloverRMS;

uses
  ComServ,
  CloverRMS_TLB in 'CloverRMS_TLB.pas',
  uTenderEnd in 'class\uTenderEnd.pas' {TenderEnd: CoClass},
  uApp in 'class\uApp.pas';

exports
  DllGetClassObject,
  DllCanUnloadNow,
  DllRegisterServer,
  DllUnregisterServer,
  DllInstall;

{$R *.TLB}

{$R *.RES}

begin
end.
