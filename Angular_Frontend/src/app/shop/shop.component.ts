import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Category } from '../shared/models/category';
import { Company } from '../shared/models/company';
import { NavigationBarService } from '../core/navigation-bar/navigation-bar.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit{
  selectedCategoryId = 0;
  selectedCompanyId = 0;
  sortBy = 'alph';
  sortingOptions = [
    {name: 'Nazwa - Alfabetycznie', value: 'alph'},
    {name: 'Cena - Od najniższej', value: 'costasc'},
    {name: 'Cena - Od najwyższej', value: 'costdesc'},
    {name: 'Data - Od najstarszej', value: 'dateasc'},
    {name: 'Data - Od najnowszej', value: 'datedesc'},
  ];
  pageSize = 12;
  pageNumber = 1;
  objectCount = 0;

  searching: string='';

  products: Product[] =[];
  categories: Category[] =[];
  companies: Company[] =[];

  constructor(private shopService: ShopService, private navBarService: NavigationBarService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
    this.getCompanies();

    this.loadNavBarSearch();
  }

  getProducts(){
    this.shopService.getProducts(this.selectedCategoryId, this.selectedCompanyId, this.sortBy, this.pageSize, this.pageNumber, this.searching).subscribe({
      next: response => {
        this.products = response.objectList,
        this.pageSize = response.pageSize,
        this.pageNumber = response.pageNumber,
        this.objectCount = response.objectCount
      },
      error: error => console.log(error),
    })
  }
  getCategories(){
    this.shopService.getCategories().subscribe({
      next: response => this.categories = [{id:0, name: 'Wszystkie'}, ...response],
      error: error => console.log(error),
    })
  }
  getCompanies(){
    this.shopService.getCompanies().subscribe({
      next: response => this.companies = [{id:0, name: 'Wszystkie'}, ...response],
      error: error => console.log(error),
    })
  }

  onPageClicked(event: any){

    if(event !== this.pageNumber){
      this.pageNumber = event;
      this.getProducts();
      window.scrollTo(0, 0);
    }
  }
  onPageSizeChanged(event: any){
      this.pageSize = event.target.value;
      this.pageNumber = 1;
      this.getProducts();
      window.scrollTo(0, 0);
  }

  onSortingSelected(event: any){
    if(event.target.value !== this.pageSize){
      this.sortBy = event.target.value;
      this.pageNumber = 1;
      this.getProducts();
      window.scrollTo(0, 0);
    }
  }
  onCategoryIdSelected(categoryId: number){
    this.selectedCategoryId = categoryId;
    this.pageNumber = 1;
    this.getProducts();
    window.scrollTo(0, 0);
  }
  onCompanyIdSelected(companyId: number){
    this.selectedCompanyId = companyId;
    this.pageNumber = 1;
    this.getProducts();
    window.scrollTo(0, 0);
  }

  loadNavBarSearch(){
    this.navBarService.newSearch.subscribe({
      next: searching =>{
        this.searching = searching;
        this.selectedCategoryId=0;
        this.selectedCompanyId=0;
        this.pageNumber = 1;
        this.getProducts();
        window.scrollTo(0, 0);
      }
    });
  }

}
