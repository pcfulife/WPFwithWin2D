#include "pch.h"
#include <d2d1_3.h>
#include <d3d11.h>
#include <inspectable.h>
#include <windows.graphics.directx.direct3d11.interop.h>

extern "C" {
	__declspec(dllexport) IUnknown* GetD3D11Device(IDXGIDevice* handle);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	return TRUE;
}

IUnknown* GetD3D11Device(IDXGIDevice* dxgiDevice) {
    IInspectable* graphicsDevice = nullptr;
    CreateDirect3D11DeviceFromDXGIDevice(dxgiDevice, &graphicsDevice);

    return graphicsDevice;
}