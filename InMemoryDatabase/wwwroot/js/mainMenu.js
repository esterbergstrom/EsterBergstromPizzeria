(function () {

    var mainMenu = document.getElementsByClassName('main-menu')[0];

    document.getElementsByClassName('main-menu-button--open')[0].addEventListener('click', function () {
        mainMenu.className = 'main-menu main-menu--visible';
    });

    document.getElementsByClassName('main-menu-button--close')[0].addEventListener('click', function () {
        mainMenu.className = 'main-menu main-menu--hidden';
    });

})();