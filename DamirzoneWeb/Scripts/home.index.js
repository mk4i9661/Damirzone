function Index(path) {
    var panel = document.getElementById("panel");
    return {
        showImage: function (img) {
            var image = document.createElement("img");
            image.onload = function () {
                panel.removeChild(panel.firstChild);
                panel.style.display = "block";

                var width = image.width,
                    height = image.height,
                    panelWidth = panel.clientWidth,
                    panelHeight = panel.clientHeight,
                    coef = Math.min(panelWidth / width, panelHeight / height);

                if (coef < 1) {
                    width *= coef; height *= coef;
                    image.style.width = width + "px";
                    image.style.height = height + "px";
                } else {
                    image.style.width = "auto";
                    image.style.height = "auto";
                }
                image.style.position = "absolute";
                image.style.left = ((panelWidth - width) / 2) + "px";
                image.style.top = ((panelHeight - height) / 2) + "px";
                panel.appendChild(image);
            };
            image.src = path + "?img=" + img;

        },
        hideImage: function () {
            panel.style.display = "none";
        }
    };
}