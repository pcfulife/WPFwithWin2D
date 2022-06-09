#pragma once

#include "Class.g.h"
#include <dxgi.h>

namespace winrt::NativeWinRT::implementation
{
    struct Class : ClassT<Class>
    {
        Class() = default;

        IUnknown* GetD3D11Device(IDXGIDevice* dxgi);
    };
}

namespace winrt::NativeWinRT::factory_implementation
{
    struct Class : ClassT<Class, implementation::Class>
    {
    };
}
