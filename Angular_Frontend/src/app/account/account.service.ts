import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Address } from '../shared/models/address';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  basePath = 'https://localhost:5002/api/';

  private loggedUserSource = new BehaviorSubject<User | null>(null);
  loggedUserSource$ = this.loggedUserSource.asObservable();

  constructor(private ruter: Router, private http: HttpClient) { }


  mailExistanceCheck(emailAddress : string){
    return this.http.get<boolean>(this.basePath + "account/email-address-exists?emailAdress=" + emailAddress);
  }

  logUserFromBrowserStorageToken(browserToken: string){
    let headers = new HttpHeaders();

    headers = headers.set("Authorization", `Bearer ${browserToken}`);

    return this.http.get<User>(this.basePath + "account", {headers}).pipe(
      map(userResponse => {
        localStorage.setItem('token', userResponse.token);

        this.loggedUserSource.next(userResponse);
      })
    );
  }

  login(params: any){
    return this.http.post<User>(this.basePath + "account/login", params).pipe(
      map(userResponse => {
        localStorage.setItem('token', userResponse.token);

        this.loggedUserSource.next(userResponse);
      })
    )

  }

  logOut(){
    localStorage.removeItem('token');
    this.loggedUserSource.next(null)

    this.ruter.navigateByUrl("/");
  }

  registration(params: any){
    return this.http.post<User>(this.basePath + "account/register", params).pipe(
      map(userResponse => {
        localStorage.setItem('token', userResponse.token);

        this.loggedUserSource.next(userResponse);
      })
    )
  }

  updateAddressOfUser(newAddress: any){
    const address : Address = {
      firstName : newAddress.firstName,
      lastName: newAddress.lastName,
      country : newAddress.country,
      voivodeship: newAddress.voivodeship,
      city: newAddress.city,
      street: newAddress.street,
      buildingNumber: newAddress.buildingNumber,
      zipCode: newAddress.zipCode,
      
    }
    return this.http.put(this.basePath + "account/shipping-address", address);
  }

  getAddressOfUser(){
    const address = this.http.get<Address>(this.basePath + "account/shipping-address");
    
    return address;
  }
}
