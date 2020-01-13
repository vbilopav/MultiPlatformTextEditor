(async function() {

    const apiUrl = "api/file";
    const title = document.getElementById("title");
    const subTitle = document.querySelector("span.small");
    const save = document.getElementById("save");
    const editor = document.getElementById("editor");
    const badge = document.querySelector(".badge");
    badge.style["display"] = "none";

    const response = await fetch(apiUrl);
    const name = response.headers.get("file-name");
    const fullName = response.headers.get("full-name");

    title.innerHTML = document.title = name;
    title.setAttribute("title", fullName);
    subTitle.innerHTML = fullName;

    editor.value = await response.text();
    editor.focus();

    save.addEventListener("click", async () => {

        await fetch(apiUrl, {
            method: "POST",
            body: editor.value,
            headers: {
                "Content-Type": "text/plain; charset=utf-8"
            }
        });
        editor.focus();
        badge.style["display"] = "";

    });

    editor.addEventListener("input", () => badge.style["display"] = "none");

})();