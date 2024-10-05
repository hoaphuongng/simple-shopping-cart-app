import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ShoppingCartItem } from '../models/ShoppingCartItem';

@Injectable({
    providedIn: 'root'
})
export class ShoppingCartService {
    private apiUrl = 'http://localhost:5000/api/shoppingcart';

    constructor(private http: HttpClient) {}

    getShoppingCartItems(): Promise<ShoppingCartItem[]> {
        return this.http.get<ShoppingCartItem[]>(this.apiUrl).toPromise();
    }
    
    getShoppingCartItemById(id: number): Promise<ShoppingCartItem> {
        return this.http.get<ShoppingCartItem>(`${this.apiUrl}/${id}`).toPromise();
    }

    addShoppingCartItem(item: ShoppingCartItem): Promise<ShoppingCartItem> {
        return this.http.post<ShoppingCartItem>(this.apiUrl, item).toPromise();
    }

    deleteShoppingCartItem(id: number): Promise<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`).toPromise();
    }

    updateShoppingCartItem(id: number, item: ShoppingCartItem): Promise<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, item).toPromise();
    }
}
