import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ShoppingCartService } from '../shopping-cart.service';


@Component({
  selector: 'display-ebook',
  templateUrl: './display-ebook.component.html',
  styleUrls: ['./display-ebook.component.css'],
  providers: [ShoppingCartService]
})


export class DisplayEbook implements OnInit {
  title = 'Ebooki w naszej ofercie';
  ebooks = Array<Ebook>();
  p: number = 1;

  constructor(private http: HttpClient, private readonly cartService: ShoppingCartService) {
  }

  ngOnInit(): void {
    this.http.get('http://localhost:53420/api/Ebook')
      .subscribe((data: Array<Ebook>) => {
        this.ebooks = data;
        console.log(this.ebooks);
      }
      );
  }

  addToCart($event, model) {
    this.cartService.addItem(model);
  }


}

interface Ebook {

  Title: string;
  Description: string;
  Price: number;
}
