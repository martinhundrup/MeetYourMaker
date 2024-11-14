mergeInto(LibraryManager.library, {
    SaveFile: function (data, filename) {
        const jsonData = UTF8ToString(data);
        const blob = new Blob([jsonData], { type: 'application/json' });
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = UTF8ToString(filename);
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }
});

mergeInto(LibraryManager.library, {
    UploadFile: function(callback) {
        const fileInput = document.createElement('input');
        fileInput.type = 'file';
        fileInput.accept = '.json';

        fileInput.onchange = (event) => {
            const file = event.target.files[0];
            if (file) {
                if (file.type !== 'application/json') {
                    alert("Please upload a valid JSON file.");
                    return;
                }
                const reader = new FileReader();
                reader.onload = (e) => {
                    const contents = e.target.result;
                    if (!contents) {
                        alert("The uploaded file is empty or not valid JSON.");
                        return;
                    }
                    const length = lengthBytesUTF8(contents) + 1;
                    const buffer = _malloc(length);
                    stringToUTF8(contents, buffer, length);
                    dynCall('vi', callback, [buffer]);
                    _free(buffer);
                };
                reader.readAsText(file);
            }
        };

        fileInput.click();
    }
});