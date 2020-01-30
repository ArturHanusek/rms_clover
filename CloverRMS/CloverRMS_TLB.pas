unit CloverRMS_TLB;

// ************************************************************************ //
// WARNING
// -------
// The types declared in this file were generated from data read from a
// Type Library. If this type library is explicitly or indirectly (via
// another type library referring to this type library) re-imported, or the
// 'Refresh' command of the Type Library Editor activated while editing the
// Type Library, the contents of this file will be regenerated and all
// manual modifications will be lost.
// ************************************************************************ //

// $Rev: 34747 $
// File generated on 11/09/2018 13:45:37 from Type Library described below.

// ************************************************************************  //
// Type Lib: E:\Dropbox\dev\clover_rms\Clover\CloverRMS\CloverRMS (1)
// LIBID: {FABAB593-8D87-4A03-A96C-D60DC1A4958E}
// LCID: 0
// Helpfile:
// HelpString:
// DepndLst:
//   (1) v2.0 stdole, (C:\Windows\system32\stdole2.tlb)
// ************************************************************************ //
{$TYPEDADDRESS OFF} // Unit must be compiled without type-checked pointers.
{$WARN SYMBOL_PLATFORM OFF}
{$WRITEABLECONST ON}
{$VARPROPSETTER ON}
{$ALIGN 4}
interface

uses Windows, ActiveX, Classes, Graphics, OleServer, StdVCL, Variants;


// *********************************************************************//
// GUIDS declared in the TypeLibrary. Following prefixes are used:
//   Type Libraries     : LIBID_xxxx
//   CoClasses          : CLASS_xxxx
//   DISPInterfaces     : DIID_xxxx
//   Non-DISP interfaces: IID_xxxx
// *********************************************************************//
const
  // TypeLibrary Major and minor versions
  CloverRMSMajorVersion = 1;
  CloverRMSMinorVersion = 0;

  LIBID_CloverRMS: TGUID = '{FABAB593-8D87-4A03-A96C-D60DC1A4958E}';

  IID_ITenderEnd: TGUID = '{C7CE13C5-5E9E-4DA1-A907-B6DC6BD9508A}';
  CLASS_TenderEnd: TGUID = '{FABC8207-361C-4740-B534-06AD9C6FB18C}';
type

// *********************************************************************//
// Forward declaration of types defined in TypeLibrary
// *********************************************************************//
  ITenderEnd = interface;
  ITenderEndDisp = dispinterface;

// *********************************************************************//
// Declaration of CoClasses defined in Type Library
// (NOTE: Here we map each CoClass to its Default Interface)
// *********************************************************************//
  TenderEnd = ITenderEnd;


// *********************************************************************//
// Interface: ITenderEnd
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {C7CE13C5-5E9E-4DA1-A907-B6DC6BD9508A}
// *********************************************************************//
  ITenderEnd = interface(IDispatch)
    ['{C7CE13C5-5E9E-4DA1-A907-B6DC6BD9508A}']
    function Process(const Session: IDispatch): WordBool; stdcall;
  end;

// *********************************************************************//
// DispIntf:  ITenderEndDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {C7CE13C5-5E9E-4DA1-A907-B6DC6BD9508A}
// *********************************************************************//
  ITenderEndDisp = dispinterface
    ['{C7CE13C5-5E9E-4DA1-A907-B6DC6BD9508A}']
    function Process(const Session: IDispatch): WordBool; dispid 201;
  end;

// *********************************************************************//
// The Class CoTenderEnd provides a Create and CreateRemote method to
// create instances of the default interface ITenderEnd exposed by
// the CoClass TenderEnd. The functions are intended to be used by
// clients wishing to automate the CoClass objects exposed by the
// server of this typelibrary.
// *********************************************************************//
  CoTenderEnd = class
    class function Create: ITenderEnd;
    class function CreateRemote(const MachineName: string): ITenderEnd;
  end;

implementation

uses ComObj;

class function CoTenderEnd.Create: ITenderEnd;
begin
  Result := CreateComObject(CLASS_TenderEnd) as ITenderEnd;
end;

class function CoTenderEnd.CreateRemote(const MachineName: string): ITenderEnd;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_TenderEnd) as ITenderEnd;
end;

end.

