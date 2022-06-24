
// Toggle Sidenav
function ToggleSidenav() {

    const iconSidenav = document.getElementById('iconSidenav');
    const sidenav = document.getElementById('sidenav-main');
    let body = document.getElementsByTagName('body')[0];
    let className = 'g-sidenav-pinned';


    if (body.classList.contains(className)) {
        body.classList.remove(className);
        setTimeout(function () {
            sidenav.classList.remove('bg-white');
        }, 100);
        sidenav.classList.remove('bg-transparent');

    } else {
        body.classList.add(className);
        sidenav.classList.add('bg-white');
        sidenav.classList.remove('bg-transparent');
        iconSidenav.classList.remove('d-none');
    }

    return 1;
}