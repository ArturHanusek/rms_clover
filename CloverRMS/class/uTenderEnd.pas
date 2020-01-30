unit uTenderEnd;

{$WARN SYMBOL_PLATFORM OFF}

interface

uses
  ComObj, ActiveX, CloverRMS_TLB, StdVcl;

type
  TTenderEnd = class(TAutoObject, ITenderEnd)
  protected
    function Process(const Session: IDispatch): WordBool; stdcall;

  end;

implementation

uses ComServ, SysUtils, uDebuger, uApp;

function TTenderEnd.Process(const Session: IDispatch): WordBool;
begin
  Debug.EnterMethod(Self, 'Process');

  try

    APP := TApp.Create;

    if Assigned(APP.OnTenderEnd) then
    Begin
      APP.SetSession(Session);
      APP.OnTenderEnd(Result);
    End;

  except
    on E: Exception do
    Begin
      Debug.ExceptionHandler(Self, E);
      APP.DoOnException(Self, E, Result);
    End;
  end;

  Debug.ExitMethodCollapse(Self, 'Process');
end;

initialization
  TAutoObjectFactory.Create(ComServer, TTenderEnd, Class_TenderEnd,
    ciMultiInstance, tmApartment);
end.
