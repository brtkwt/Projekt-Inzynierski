import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Category } from '../shared/models/category';
import { Company } from '../shared/models/company';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  basePath = 'https://localhost:5002/api/'

  constructor(private http: HttpClient) { }
  
  getProducts(categoryId?: number, companyId?: number, sortBy?: string, pageSize?: number, pageNumber?: number, searching?: string) {
    let parameters = new HttpParams();

    if(categoryId && categoryId > 0){
      parameters = parameters.append('categoryId', categoryId);
    }
    if(companyId && companyId > 0){
      parameters = parameters.append('companyId', companyId);
    }
    if(sortBy){
      parameters = parameters.append('sortBy', sortBy);
    }
    if(pageSize){
      parameters = parameters.append('pageSize', pageSize);
    }
    if(pageNumber){
      parameters = parameters.append('pageNumber', pageNumber);
    }
    if(searching){
      parameters = parameters.append('nameSearch',searching);
    }

    return this.http.get<Pagination<Product[]>>(this.basePath + 'product', {params: parameters});
  }
  getCategories(){
    return this.http.get<Category[]>(this.basePath + 'category');
  }
  getCompanies(){
    return this.http.get<Company[]>(this.basePath + 'company');
  }

  getProductInfo(id: number){
    return this.http.get<Product>(this.basePath + 'product/' + id);
  }
}
