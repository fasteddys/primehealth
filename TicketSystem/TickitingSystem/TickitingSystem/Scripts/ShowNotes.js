window.onload = appandnotes();



function appandnotes() {
    var numItems = $('.appandnote').length;
    for (var i = 0; i < numItems; i++) {
        var item1 = document.getElementsByClassName("appandnote")[i];
        if (item1 != null) {
            let item = document.getElementsByClassName("appandnote")[i].textContent;

            var parent = item1.parentElement;
            // parent.removeChild(parent.childNodes[0]);
            // parent.textContent = "";
            $(parent).append(item);
            item1.textContent = '';
            item1.style.display = "none";
        }

    }

}