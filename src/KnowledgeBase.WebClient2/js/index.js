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
<div class="col">
    <div class="card" style="width: 18rem;">
  <div class="card-body">
    <h5 class="card-title">${item.title}</h5>
    <p class="card-text">${item.content}</p>
  </div>
</div>
</div>
`;
  }
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
    (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
  );
}
