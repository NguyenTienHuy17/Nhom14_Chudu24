const active_contact = () => {
    document.querySelector('.container-contact').classList.add('active');
    document.querySelector('.bg-action').classList.add('display');
}
const remove_contact = () => {
    document.querySelector('.container-contact').classList.remove('active');
    document.querySelector('.bg-action').classList.remove('display');
}