#include "pch.h"
#include <d2d1_3.h>
#include <d3d11.h>

extern "C" {
	__declspec(dllexport) IUnknown* GetD3D11Device(IDXGIDevice* handle);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	return TRUE;
}

IUnknown* GetD3D11Device(IDXGIDevice* dxgi) {
	ID3D11Device* d3d11Dev = nullptr;
	dxgi->QueryInterface(&d3d11Dev);

	return d3d11Dev;
}