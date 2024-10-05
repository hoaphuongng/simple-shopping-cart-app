import { Component, OnInit } from '@angular/core';
import { ShoppingCartItem } from '../models/ShoppingCartItem';
import { ShoppingCartService } from '../services/shopping-cart.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-shopping-cart',
    templateUrl: './shopping-cart.component.html',
    styleUrls: ['./shopping-cart.component.css'],
})
export class ShoppingCartComponent implements OnInit {
    
    items: ShoppingCartItem[] = [];
    shoppingCartForm: FormGroup;
    updatedItem: ShoppingCartItem | null = null;

    constructor(
        private shoppingCartService: ShoppingCartService,
        private fb: FormBuilder
    ) {
        this.shoppingCartForm = this.fb.group({
            name: ['', Validators.required],
            quantity: ['', [Validators.required, Validators.min(1)]],
            price: ['', [Validators.required, Validators.min(0.05)]]
        });
    }

    ngOnInit(): void {
        this.fetchItems();
    }

    private fetchItems(): void {
        this.shoppingCartService.getShoppingCartItems().then(items => {
            this.items = items;
        })
    }

    deleteItem(id: number): void {
        this.shoppingCartService.deleteShoppingCartItem(id).then(() => {
            this.fetchItems();
        });
    }

    addItem(): void {
        const newItem: ShoppingCartItem = this.shoppingCartForm.value;

        this.shoppingCartService.addShoppingCartItem(newItem).then(() => {
            this.fetchItems();
            this.resetForm();
        });
    }

    updateItem(): void {
        
        if (this.updatedItem != null) {
            const updatedItem: ShoppingCartItem = this.shoppingCartForm.value;

            this.shoppingCartService.updateShoppingCartItem(this.updatedItem.id, updatedItem).then(() => {
                this.fetchItems();
                this.resetForm();
            });
        }
    }

    setUpdatedItem(item: ShoppingCartItem | null): void {
        this.updatedItem = item;
        
        if (this.updatedItem != null) {
            this.shoppingCartForm.patchValue(this.updatedItem);
        }
    }

    resetForm(): void {
        this.setUpdatedItem(null);
        this.shoppingCartForm.reset();
    }
}
