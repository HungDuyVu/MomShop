import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateOrEditWeatherComponent } from './create-or-edit-weather/create-or-edit-weather.component';
interface Product {
  Id?: number;
  name?: string;
  category?: string;
  quantity?: number;
}
@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.scss']
})
export class WeatherComponent implements OnInit {
  @ViewChild('Create', { static: true }) createWeather: CreateOrEditWeatherComponent;
  weatherData: any;
  tableData: any;
  cols: any[] = [];
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.cols = [
      {
          field: 'id',
          header: '#Id'
      },
      {
          field: 'name',
          header: 'Name'
      },
      {
          field: 'category',
          header: 'Category'
      },
      {
        field: 'quantity',
        header: 'Quantity'
    },
  ];
    this.getProductData();
  }

  getWeatherData(): void {
    const url = 'http://localhost:5000/WeatherForecast';
    this.http.get(url).subscribe(
      data => {
        this.weatherData = data;
      },
      error => {
        console.error(error);
      }
    );
  }

  getProductData(): void {
    const url = 'http://localhost:5001/api/product/find-all';
    this.http.get(url).subscribe(
      data => {
        this.tableData = data;
        console.log(data);
      },
      error => {
        console.error(error);
      }
    );
  }
  onSelectionChange(event){

  }
  Creates(){
    this.createWeather.show();
  }
}
