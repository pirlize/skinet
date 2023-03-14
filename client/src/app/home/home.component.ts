import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public animal1: IAnimal = new Dog('roufus', 35);
  public animal2: IAnimal = new Cat('psin', 22);
  ngOnInit(): void {
    this.animal1.sound();
    this.animal2.sound();
  }
}

export interface IAnimal {
  name: string;
  age: number;
  sound(): void;
}

export class Dog implements IAnimal {
  name: string;
  age: number;
  constructor(name: string, age: number) {
    this.name = name;
    this.age = age;
  }
  sound(): void {
    console.log('im dogo woof!');
  }
}

export class Cat implements IAnimal {
  name: string;
  age: number;
  constructor(name: string, age: number) {
    this.name = name;
    this.age = age;
  }
  sound(): void {
    console.log('im cat meow!');
  }
}
