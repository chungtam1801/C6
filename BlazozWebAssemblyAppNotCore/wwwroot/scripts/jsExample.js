export function showAlert(obj) {
    alert('Name: '+obj.name+', Age: '+obj.age);
};
export function emailRegistration(message) {
    const result = prompt(message);
    if (result === '' || result === null) return 'Please provide an email';
    const returnMessage = 'Hi ' + result.split('@')[0] + ', your email: ' + result + ' has been accepted';
    return returnMessage;
};
export function splitEmailDetail(message) {
    const email = prompt(message);
    if (email === '' || email === null) return null;
    const firstPart = email.substring(0, email.indexOf('@'));
    const secondPart = email.substring(email.indexOf('@') + 1);
    return {
        'name': firstPart,
        'server': secondPart.split('.')[0],
        'domain': secondPart.split('.')[1]
    };
}