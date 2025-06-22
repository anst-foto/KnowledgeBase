const url = 'http://localhost:5196/api/v1/articles';

document.addEventListener("DOMContentLoaded", loadArticles);

let formInput = document.getElementById("formInput");
formInput.addEventListener("submit", async (e) => {
  e.preventDefault();

  let title = document.getElementById("title").value;
  let content = document.getElementById("content").value;

  let response = await fetch(url, {
    method: "POST",
    headers: {
      'Content-Type': 'application/json;charset=utf-8'
    },
    body: JSON.stringify({
      id: uuidv4(),
      title: title,
      tags: [
        "string"
      ],
      content: content,
      dateOfCreation: "2025-06-22T17:23:12.377Z",
      dateOfLastUpdate: "2025-06-22T17:23:12.377Z",
      isDeleted: true
    })
  });
  let result = await response.json();
  console.debug(result);
});


async function loadArticles() {
  let articlesElement = document.getElementById('articles');

  let response = await fetch(url, {
    method: 'GET',
  });
  let json = await response.json();

  articlesElement.innerHTML = '';
  for (let item of json) {
    articlesElement.innerHTML += `
<div class="bg-white border border-gray-300 p-6 rounded-lg shadow-sm w-6">
  <h3 class="font-bold text-xl mb-2 text-[#808080]">${item.title}</h3>
  <p class="text-gray-600">${item.content}</p>
</div>`;
  }
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
    (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
  );
}
