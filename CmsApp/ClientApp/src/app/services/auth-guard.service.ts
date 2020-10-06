
import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild, CanLoad, Route } from '@angular/router';
import { AuthService } from './auth.service';


@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private authService: AuthService, private router: Router) { }

  // กำนหนด guard ในส่วนของการใช้งานกับ  canActivate
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    console.log("canActivate run");
    const url: string = state.url;   // เก็บ url ที่พยายามจะเข้าใช้งาน

    //จะผ่านเข้าใช้งานได้เมื่อ คืนค่าเป็น true โดยเข้าไปเช็คค่าจากคำสั่ง checkLogin()
    return this.checkLogin(url);     // คืนค่าการตรวจสอบสถานะการล็อกอิน
  }

  // กำนหนด guard ในส่วนของการใช้งานกับ  canActivateChild ส่วนนี้จะใช้กับ path ของ route ย่อย
  // ถ้าเข้าผ่าน route path ย่อย guard จะเข้ามาเช็คในส่วนนี้ก่อน กลับไปเช็คในส่วนของ canActivate()
  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    console.log("canActivateChid");
    // จะเข้าใช้งานได้เมื่อ คืนค่าเป็น true โดยจะใช้ค่าจากการเรียกใช้คำสั่ง canActivate()
    return this.canActivate(route, state);
  }

  //CanLoad สำหรับเป็นตัวกลางพิจารณาการเข้าถึง route path โดยจะมีการโหลด module ของ route path ที่จะเข้าใช้งาน ภายหลัง
  canLoad(route: Route): boolean {

    const url = `/${route.path}`;
    return this.checkLogin(url);
  }

 // ฟังก์ชั่นเช็คสถานะการล็อกอิน รับค่า url ที่ผู้ใช้พยายามจะเข้าใช้งาน
  checkLogin(url: string): boolean {

    // login สำเร็จ  ถ้าตรวจสอบค่าสถานะการล็อกอินแล้วเป็น true ก็ให้คืนค่า true กลับออกไป
    if (this.authService.isLoggedIn)
    {
      console.log("check login return = true...");
      return true;
    }

    // แต่ถ้ายังไม่ได้ล็อกอิน ให้เก็บ url ที่พยายามจะเข้าใช้งาน สำหรับ link เปลี่ยนหน้า
    this.authService.loginRedirectUrl = url;   // redirectUrl เป็นตัวแปรที่อยู่ใน authService
    this.router.navigate(['/login']); // ลิ้งค์ไปยังหน้าล็อกอิน เพื่อล็อกอินเข้าใช้งานก่อน
    // คืนค่า false กรณียังไม่ได้ล็อกอิน
    console.log("check login return false");
    return false;
  }
}
