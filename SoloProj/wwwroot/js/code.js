// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// SELECTORS
let img = document.querySelector("img");
let btn = document.querySelector("button");

// OPTION A - Async/Await (see the README for an alternative way to write this function)
async function getNewDogImg() {
  let response = await fetch("https://random.dog/woof.json"); // fetch gets data from an API that is in a JSON format, that data is then stored in the response variable
  let data = await response.json(); // .json() converts the response JSON data to usable data, this usable data is stored in the data variable
  img.src = data.url; // image.src is set equal to data.url to render the new dog image
}

// EVENT LISTENERS
btn.addEventListener("click", () => {
  getNewDogImg(); // make a new fetch call that updates img
});

// GET INITIAL DOG PHOTO ON PAGE LOAD/REFRESH
getNewDogImg();

// fetch("https://random.dog/woof.json", {
//   method: "GET",
//   headers: null,
//   body: null,
// });
