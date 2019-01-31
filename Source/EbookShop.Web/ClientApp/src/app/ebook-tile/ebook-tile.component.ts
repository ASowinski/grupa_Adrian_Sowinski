import { Component, OnInit, Input,} from '@angular/core';
import { ShoppingCartService } from '../shopping-cart.service';

@Component({
  selector: 'ebook-tile',
  templateUrl: './ebook-tile.component.html',
  styleUrls: ['./ebook-tile.component.css'],
  providers: [ShoppingCartService]
})
export class EbookTileComponent implements OnInit {

  @Input() ebookModel: any;
  @Input() buttonCallback: Function;
  @Input() buttonText: string;


  constructor(private readonly cartService: ShoppingCartService) { }

  ngOnInit() {
  }

  onClick($event, model) {
    this.buttonCallback($event, model);
  }
  getAuthors() {
    let a: string[] = this.ebookModel.authors.map(a => a.fullName);
    return a.join(" ");
  }

  getSrc() {
    let a = 'http://localhost:53420/';
    let b: string = this.ebookModel.files.map(f => f.path); 
    return a.concat(b);
  }
}
