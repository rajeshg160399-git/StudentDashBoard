/**
 * courses.js - Module-specific JavaScript for Courses management
 * Handles course interactive features, filtering, and event listeners.
 */

document.addEventListener('DOMContentLoaded', () => {
    initCourseSearch();
});

/**
 * Initializes interactive search and filter behavior for courses.
 */
function initCourseSearch() {
    const courseSearchInput = document.getElementById('courseSearchInput');
    if (!courseSearchInput) return;

    courseSearchInput.addEventListener('input', (e) => {
        const query = e.target.value.toLowerCase().trim();
        filterCourses(query);
    });
}

/**
 * Filters visible course cards or list items based on keyword.
 * @param {string} query
 */
function filterCourses(query) {
    const courseItems = document.querySelectorAll('.course-item');
    courseItems.forEach((item) => {
        const title = item.textContent.toLowerCase();
        item.style.display = !query || title.includes(query) ? '' : 'none';
    });
    var money = CreateMoneyBox();
    var one = money();
    var two = money();

    
    PrototypalInheritance();
    CallApplyBind();

    DisPlayData();

}

//Closure
function CreateMoneyBox(){
    let money = 0;
    return function(){
        money+=10;
        return money
    }
    //this Key word
const student={
    name:'Raj',
    displayName: function(){
        console.log(this.name);
    }
}
student.displayName();

}

function greet(message,symbol){

    console.log(message +this.name+ symbol)


}

//call apply Bind
function CallApplyBind(){
const newStudent = {name:'Raj'}
greet.call(newStudent,'hellow','!')

greet.apply(newStudent,['hellow','!'])
const my= greet.bind(newStudent,'hellow','!')
my('!')
}


//Prototypal Inheritance
function PrototypalInheritance(){
const Student = {
    Greet: function(){
        console.log("Student greeted")

    }
}
const person = {
    name:'Raj'
}
Object.setPrototypeOf(person,Student)
person.Greet();
person.name;

}

function GetData(){
    return new promise((resolve)=>{
        console.log("Data Recived")
    })
}

async function DisplayData(){
    var result = await GetData();
        console.log("result")

}

