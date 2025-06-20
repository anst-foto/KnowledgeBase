let articlesElement = document.getElementById('articles');

const url = 'http://localhost:5196/api/v1/articles';
let response = await fetch(url, {
  method: 'GET',
});
let json = await response.json();

articlesElement.innerHTML = '';
for (let item of json) {
articlesElement.innerHTML += `<div class="flex flex-col justify-around">`;
articlesElement.innerHTML += `<p>${item.id}</p>`;
articlesElement.innerHTML += `<p>${item.title}</p>`;
articlesElement.innerHTML += `</div>`;
}
