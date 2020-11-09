
import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild, CanLoad, Route } from '@angular/router';
import { AuthService } from './auth.service';
//import { log } from 'util';

//ถ้า Return true ผู้ใช้สามารถเข้าถึง Component ได้
//ถ้า Return false การเข้าถึง Component นั้นจะถูกหยุด และไม่ทำอะไร (ยกเว้นว่าเราจะสั่งให้ Route ส่งผู้ใช้ไปที่อื่นแทน)
//ถ้า Return UrlTree การเข้าถึงจะถูกหยุดแล้วไปใช้ UrlTree แทน

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {

  constructor(private authService: AuthService, private router: Router) { }

  //*********CanActivate ใช้ตัดสินใจว่า Route นั้นสามารถเข้าถึงได้หรือไม่***************
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    const url: string = state.url;   // เก็บ url ที่พยายามจะเข้าใช้งาน
    console.log("auth-guard.service.ts func canActivate for url => " + url);

    //จะผ่านเข้าใช้งานได้เมื่อ คืนค่าเป็น true โดยเข้าไปเช็คค่าจากคำสั่ง checkLogin()
    return this.checkLogin(url);     // คืนค่าการตรวจสอบสถานะการล็อกอิน
  }

 //*******CanActivateChild ใช้ตัดสินใจว่า Route Child นั้นสามารถเข้าถึงได้หรือไม่*******
  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    console.log("auth-guard.service.ts func canActivateChild.....");
    // จะเข้าใช้งานได้เมื่อ คืนค่าเป็น true โดยจะใช้ค่าจากการเรียกใช้คำสั่ง canActivate()
    return this.canActivate(route, state);
  }

  //******CanLoad สำหรับเป็นตัวกลางพิจารณาการเข้าถึง route path โดยจะมีการโหลด module ของ route path ที่จะเข้าใช้งาน ภายหลัง******
  canLoad(route: Route): boolean {
    console.log("auth-guard.service.ts func canload for route path....");
    const url = `/${route.path}`;
    console.log("auth-guard.service.ts func canload for url => " + url);
    return this.checkLogin(url);
  }

 // ฟังก์ชั่นเช็คสถานะการล็อกอิน รับค่า url ที่ผู้ใช้พยายามจะเข้าใช้งาน
  checkLogin(url: string): boolean {

    console.log("auth-guard.service.ts func checkLogin.....");
    //login สำเร็จ  ถ้าตรวจสอบค่าสถานะการล็อกอินแล้วเป็น true ก็ให้คืนค่า true กลับออกไป
    if (this.authService.isLoggedIn)
    {
      console.log("auth-guard.service.ts func checklogin for check authService.isLoggedIn (Return true.....)");
      return true;
    }
    console.log("auth-guard.service.ts func checklogin for check authService.isLoggedIn (Return false.....)");
    //แต่ถ้ายังไม่ได้ล็อกอิน ให้เก็บ url ที่พยายามจะเข้าใช้งาน สำหรับ link เปลี่ยนหน้า
    this.authService.loginRedirectUrl = url;  // redirectUrl เป็นตัวแปรที่อยู่ใน authService

    this.router.navigate(['/login']);         //Link ไปยังหน้าล็อกอิน เพื่อล็อกอินเข้าใช้งานก่อน
    console.log("auth-guard.service.ts func checkLogin to redirect /login");

    // คืนค่า false กรณียังไม่ได้ล็อกอิน
    return false;
  }
}
