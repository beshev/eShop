const images = document.querySelectorAll(".image-modal");
let imgIndex
let imgSrc;

// get images src onclick
images.forEach((img, i) => {
    img.addEventListener("click", (e) => {
        imgSrc = e.target.getAttribute('src');
        //run modal function
        imgModal(imgSrc);
        //index of the next image
        imgIndex = i;
    });
});

//creating the modal
let imgModal = (src) => {
    const modal = document.createElement("div");
    modal.setAttribute("class", "modal");
    //add modal to the parent element
    document.querySelector(".main-font").append(modal);
    //adding image to modal
    const newImage = document.createElement("img");
    newImage.setAttribute("src", src);
    //creating the close button
    const closeBtn = document.createElement("i");
    closeBtn.setAttribute("class", "fas fa-times closeBtn");
    //close function
    closeBtn.onclick = () => {
        modal.remove();
    };

    modal.append(newImage, closeBtn);
};


let baseImage = document.getElementById('base-image');
let additionalImages = [...document.querySelectorAll('.additiona-image')].map(x => x.src);
let imagesUrls = [baseImage.href.baseVal, ...additionalImages];
let nextImage = document.getElementById('next-image');
let prevImage = document.getElementById('prev-image');
let counter = 0;

nextImage.addEventListener('click', () => {
    counter++
    if (counter >= imagesUrls.length) {
        counter = 0;
    }

    baseImage.href.baseVal = imagesUrls[counter];
})

prevImage.addEventListener('click', () => {
    counter--
    if (counter < 0) {
        counter = imagesUrls.length - 1;
    }

    baseImage.href.baseVal = imagesUrls[counter];
})