using DEPUI.Models.SchemaTools.SchemaViewer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DEPUI.Models.Layout
{
    public record MultiStepComponentContext<RequestType>
    {
        public RequestType? Request { get; set; }
        public IBrowserFile? InputFile { get; set; }
        public string? InputFileName { get; set; }
        public bool Loading { get; set; }

        public Func<MultiStepComponentContext<RequestType>, Models.Layout.MultiStepComponentStep, bool>? NextStepActive { get; set; }
        public Func<MultiStepComponentContext<RequestType>, Models.Layout.MultiStepComponentStep, bool>? PreviousStepActive { get; set; }
        public Func<MultiStepComponentContext<RequestType>, Models.Layout.MultiStepComponentStep,EventCallback, Task<string?>>? OnNextStep { get; set; }
    }
}
