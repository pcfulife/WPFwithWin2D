#include "pch.h"
#include "Class.h"
#include "Class.g.cpp"
#include <d3d11.h>
#include <inspectable.h>
#include <windows.graphics.directx.direct3d11.interop.h>

namespace winrt::NativeWinRT::implementation
{
    IUnknown* Class::GetD3D11Device(IDXGIDevice* dxgiDevice) {
        IInspectable* graphicsDevice = nullptr;
        CreateDirect3D11DeviceFromDXGIDevice(dxgiDevice, &graphicsDevice);

        return graphicsDevice;
    }
}
