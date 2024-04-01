

function getScrollHeight(element) {
    return element.scrollHeight;
}

window.getScrollHeight = getScrollHeight;

function selectElement(element) {
    element.click();
}

window.selectElement = selectElement;

async function downloadFileFromStream (fileName, contentStreamReference)  {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}

window.downloadFileFromStream = downloadFileFromStream;