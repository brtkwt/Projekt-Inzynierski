import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ShippingMethod } from '../shared/models/shippingMethod';
import { FinalizationService } from './finalization.service';
import { AccountService } from '../account/account.service';
import { ToastrService } from 'ngx-toastr';
import { CartService } from '../cart/cart.service';
import { Cart } from '../shared/models/cart';
import { Address } from '../shared/models/address';
import { delay } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-finalization',
  templateUrl: './finalization.component.html',
  styleUrls: ['./finalization.component.scss']
})
export class FinalizationComponent implements OnInit{
  
  shippingMethods: ShippingMethod[] = [];

  constructor(private acService: AccountService, private finService: FinalizationService, private tostS: ToastrService, public cs: CartService, private ruter: Router) {}

  ngOnInit(): void {
    this.getAddressFromApi();

    this.finService.getShippingMethods().subscribe({
      next: s => this.shippingMethods = s
    })
  }

  addShippingFee(shippingMethod: ShippingMethod){
    this.cs.addShippingFee(shippingMethod);
  }

  newFinalizationForm =  new FormGroup({
    
    firstName: new FormControl("", Validators.required),
    lastName: new FormControl("", Validators.required),
    country: new FormControl("", Validators.required),
    voivodeship: new FormControl("", Validators.required),
    city: new FormControl("", Validators.required),
    street: new FormControl("", Validators.required),
    buildingNumber: new FormControl("", Validators.required),
    zipCode: new FormControl("", Validators.required),

    shippingMethod: new FormControl(0, Validators.required),
  })

  getAddressFromApi(){
    this.acService.getAddressOfUser()?.subscribe({
      next: a => {
        a && this.newFinalizationForm?.patchValue(a);
      }
    })
  }

  updateAddressOfUser(){
    this.acService.updateAddressOfUser(this.newFinalizationForm.value).subscribe({
      next: () => this.tostS.success("Adres zapisany")
    })
  }

  private ordrCreating(cart: Cart) {
    const shippingMethodId = this.newFinalizationForm.get('shippingMethod')?.value ?? 0;

    const shippingAddress = {
      FirstName : this.newFinalizationForm.get('firstName')?.value ?? " " ,
      LastName: this.newFinalizationForm.get('lastName')?.value ?? " " ,
      Country : this.newFinalizationForm.get('country')?.value ?? " " ,
      Voivodeship: this.newFinalizationForm.get('voivodeship')?.value ?? " " ,
      City: this.newFinalizationForm.get('city')?.value ?? " " ,
      Street: this.newFinalizationForm.get('street')?.value ?? " " ,
      BuildingNumber: this.newFinalizationForm.get('buildingNumber')?.value ?? " ",
      ZipCode: this.newFinalizationForm.get('zipCode')?.value ?? " "
    }

    return{
      cartId: cart.id,
      shippingMethodId: shippingMethodId,
      ShippingAddressDto: shippingAddress,
    }
  }

  createNewOrder(){
    const cart = this.cs.getCart();

    if(!cart){
      return;
    }

    const newOrder = this.ordrCreating(cart);

    if(!newOrder){
      return;
    }
    
    this.finService.createNewOrder(newOrder).subscribe({
      next: o => {
        this.tostS.success("Zamówienie złożone!")
        this.cs.deleteCartAfterNewOrder()
        // console.log(o)

        // this.ruter.navigateByUrl("/koszyk")
      }
    })
  }
  

}
