import { Component, ElementRef, AfterViewInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements AfterViewInit {
  @ViewChild('mapContainer', { static: false }) mapContainer!: ElementRef;

  ngAfterViewInit() {
    this.loadMap();
  }

  async loadMap() {
    const position = { lat: 32.661976, lng: -16.893463 };
    //@ts-ignore
    const { Map } = await google.maps.importLibrary("maps") as google.maps.MapsLibrary;
    //@ts-ignore
    const { AdvancedMarkerElement } = await google.maps.importLibrary("marker") as google.maps.MarkerLibrary;

    const map = new Map(this.mapContainer.nativeElement, {
      zoom: 15,
      center: position,
      mapId: 'DEMO_MAP_ID',
    });

    const marker = new AdvancedMarkerElement({
      map: map,
      position: position,
      title: 'Uluru'
    });
  }
}
