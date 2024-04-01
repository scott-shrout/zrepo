using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
namespace DEPUI.Extensions
{
    [EventHandler("onmouseenter", typeof(MouseEventArgs), enableStopPropagation: true, enablePreventDefault: true)]

    [EventHandler("onmouseleave", typeof(MouseEventArgs),enableStopPropagation:true,enablePreventDefault:true)]
    public static class EventHandlers
    {

    }
}
