var myApp = angular.module("myModule", []);
myApp.controller("myController", function ($scope,$http) {
    $scope.message = "Hi Angular !";
    $scope.Exactmatch = true;
    $http.get('/ResultPublishSetting/MidMarkList').then(function (result) {
        console.log(JSON.stringify(result.data))
       // alert(JSON.stringify(result.data));
        $scope.MidListData = result.data;
    });

        $http.get('/ResultPublishSetting/ResultPublish').then(function (result) {
            console.log(JSON.stringify(result.data))
            // alert(JSON.stringify(result.data));
            $scope.ListData = result.data;
            //$(List).append(JSON.stringify(result.data));  
        });

       //SemesterList
        $http.get('/ResultPublishSetting/SemesterList').then(function (result) {
            console.log(JSON.stringify(result.data))
           //alert(JSON.stringify(result.data));
            $scope.SemesterList = result.data; 
        });

        $scope.PublishMidMark = function (object) {
            // $http.get("/Home/DeleteById", Id);
            $http({
                url: "/ResultPublishSetting/PublishMidMark",
                // params: {ListData: object},
                method: "post",
                data: JSON.stringify(object)
            }).then(function (status) {
                console.log(status);
                alert(status.data);
                //$scope.LoadList();
            })
        }
    //$http.get('/Home/TypeList').then(function (result) {
    //    console.log(JSON.stringify(result.data))
    //    $scope.Type = result.data;
    //});
    //$scope.Update = function (data) {
    //    $scope.Emp = null;
    //    $scope.Emp = data;
    //}
    //$scope.LoadList();
    //$scope.message = "Hi Angular !";
    //$scope.AddEmployee = function () {
    //    EmployeeService.AddEmployeToDb($scope.Emp);

    //}

})
//myApp.factory("EmployeeService", ['$http', function ($http) {
//    var fac = {};
//    fac.AddEmployeToDb = function (Emp, $scope) {
//        $http.post("/Home/AngularList", Emp).then(function (status) {
//            alert("Success");
//            $scope.LoadList();
//        })
//    }
//    return fac;
//}])


//Final result
var myApp = angular.module("myModuleForFinal", []);
myApp.controller("myControllerForFinal", function ($scope, $http) {
    $scope.message = "Select Batch To Publish Final Result: ";
    $scope.Exactmatch=true;

    //BatchList
    $http.get('/ResultPublishSetting/GetPublishMidList').then(function (result) {
        console.log(JSON.stringify(result.data))
        //alert(JSON.stringify(result.data));
        $scope.BatchList = result.data;
    });
        $http.get('/ResultPublishSetting/FinalMarkList').then(function (result) {
            console.log(JSON.stringify(result.data))
            // alert(JSON.stringify(result.data));
            $scope.FinalListData = result.data;
        });

    //All Table List
    $http.get('/ResultPublishSetting/ListOfMark').then(function (result) {
        console.log(JSON.stringify(result.data))
        //alert(JSON.stringify(result.data));
        $scope.TableList = result.data;
    });
    //PublishFinalMark
    $scope.PublishFinalMark = function (object) {
        $http({
            url: "/ResultPublishSetting/PublisFinalMark",
            method: "post",
            data: JSON.stringify(object)
        }).then(function (status) {
            console.log(status);
            alert(status.data);
        })
    }
})

//Assin Course ListOfCourse
var myApp = angular.module("myModuleForAssinCourse", []);
myApp.controller("myControllerForAssinCourse", function ($scope, $http) {
    $scope.message = "It,s Work";
    $scope.Exactmatch = true;

    
    //BatchList
    $http.get('/CourseSetting/ListOfCourse').then(function (result) {
        console.log(JSON.stringify(result.data))
        //alert(JSON.stringify(result.data));
        $scope.ListOfAssinCourseList = result.data;

       
    });




    $scope.AssinCourse = {
        Batch: "",
        Dept: "",
        Semester: "",
        CourseCode: "",
        CourseTitle: "",
        TeacherName: "",
        Degicnetion: "",
        Spciallity:""
    };
    //submitAssinForm
    $scope.submitAssinForm = function (AssinCourse) {
      
        if (AssinCourse.Option == null || AssinCourse.Option == undefined || AssinCourse.Option == "" || AssinCourse.Option.lenght == 0) {
            $.notify("You Must Select The All Proparty....");
        }
        else {
            if (AssinCourse.Option.Batch == null || AssinCourse.Option.Batch == undefined || AssinCourse.Option.Batch == "" || AssinCourse.Option.Batch.lenght == 0) { $.notify("Batch Is Not Select"); }
            else {
                $scope.AssinCourse.Batch = AssinCourse.Option.Batch.Batch;
                $scope.AssinCourse.Semester = AssinCourse.Option.Batch.Semester;
            }
            if (AssinCourse.Option.Dept == null || AssinCourse.Option.Dept == undefined || AssinCourse.Option.Dept == "" || AssinCourse.Option.Dept.lenght == 0) { $.notify("Dept Is Not Select"); }
            else {
                $scope.AssinCourse.Dept = AssinCourse.Option.Dept.Dept;
            }
            if (AssinCourse.Option.CourseCode == null || AssinCourse.Option.CourseCode == undefined || AssinCourse.Option.CourseCode == "" || AssinCourse.Option.CourseCode.lenght == 0) { $.notify("CourseCode Is Not Select"); }
            else {
                $scope.AssinCourse.CourseCode = AssinCourse.Option.CourseCode.CourseCode;
                $scope.AssinCourse.CourseTitle = AssinCourse.Option.CourseCode.CourseTitle;
            }
            if (AssinCourse.Option.Name == null || AssinCourse.Option.Name == undefined || AssinCourse.Option.Name == "" || AssinCourse.Option.Name.lenght == 0) { $.notify("Teacher Is Not Select"); }
            else {
                $scope.AssinCourse.TeacherName = AssinCourse.Option.Name.Name;
                $scope.AssinCourse.Degicnetion = AssinCourse.Option.Name.Type;
                $scope.AssinCourse.Spciallity = AssinCourse.Option.Name.Spciallity;
            }

            $http.post("/CourseSetting/AddEdit", AssinCourse).then(function (status) {
                $.notify("The Course Assin to The Teacher, Congratulation !");

            })

           
        }

    }
  
})

//MidResultStdView

var myApp = angular.module("myStudentModule", []);
myApp.controller("myStudentController", function ($scope, $http) {
    $scope.message = "Mid-Term Result";
    $scope.Exactmatch = true;
    $scope.Sem = {SemesterName:""};
    $scope.semesterNames = ["Semester 1", "Semester 2", "Semester 3", "Semester 4", "Semester 5", "Semester 6", "Semester 7", "Semester 8", "Semester 9", "Semester 10", "Semester 11", "Semester 12"];

    $scope.myPrint = function () {
        window.print();
    }

//for mid
    $scope.submitSemester = function (Semester) {
        $scope.Sem.SemesterName = Semester;
        if ($scope.Semester == undefined) {
            alert("Plese Select the Semester !");
        }
        else {
            $http.post("/StudentResult/ListOfMidResultStd", JSON.stringify($scope.Sem)).then(function (result) {
               // alert(JSON.stringify(result.data));
                $scope.ListData = result.data;
            });;
        }

    }

    //for final 

    $scope.submitSemesterFinal = function (Semester) {
        $scope.Sem.SemesterName = Semester;
        if ($scope.Semester == undefined) {
            alert("Plese Select the Semester !");
        }
        else {
            $http.post("/StudentResult/ListOfFinalResultStd", JSON.stringify($scope.Sem)).then(function (result) {
                // alert(JSON.stringify(result.data));
                $scope.ListData = result.data;
            });;
        }

    }

      //$http.get('/StudentResult/ListOfMidResultStd').then(function (result) {
      // console.log(JSON.stringify(result.data))
      // alert(JSON.stringify(result.data));
      // $scope.ListData = result.data;
      // $(List).append(JSON.stringify(result.data));  
      //});

    ////SemesterList
    //$http.get('/ResultPublishSetting/SemesterList').then(function (result) {
    //    console.log(JSON.stringify(result.data))
    //    //alert(JSON.stringify(result.data));
    //    $scope.SemesterList = result.data;
    //});

    //$scope.PublishMidMark = function (object) {
    //    // $http.get("/Home/DeleteById", Id);
    //    $http({
    //        url: "/ResultPublishSetting/PublishMidMark",
    //        // params: {ListData: object},
    //        method: "post",
    //        data: JSON.stringify(object)
    //    }).then(function (status) {
    //        console.log(status);
    //        alert(status.data);
    //        //$scope.LoadList();
    //    })
    //}
    

})


//for Notice Controller
var myAppForNotice = angular.module("noticeBoard", []);
myAppForNotice.controller("noticeController", function ($scope, $http) {

   // $scope.messageNotice = "Hi Angular !";

    $http.get('/NoticeBoard/GetNoticeData').then(function (result) {
        
        //alert(JSON.stringify(result.data));
        $scope.noticeBropdown = result.data;
    });


    //objest assin for c# controller send to
    $scope.Notice = {
        Batch: "",
        Dept: "",
        Semester: "",
        CourseCode: "",
        CourseName: "",
        NoticeTitle: "",
        NoticeBody: ""
    };

    $scope.submitNotice = function (Notice) {

       

        if (Notice.Option.title == null || Notice.Option.title == undefined || Notice.Option.title == "" || Notice.Option.title.lenght == 0) {
          
            $.notify("You Must Write The Notice-Title");
        }
        else {
            $scope.Notice.NoticeTitle = Notice.Option.title;
        }

        if (Notice.Option.body == null || Notice.Option.body == undefined || Notice.Option.body == "" || Notice.Option.body.lenght == 0) {

            $.notify("You Must Write The Notice-Body");
        }
        else {
            $scope.Notice.NoticeBody = Notice.Option.body;
        }


        if (Notice.Option.Subject == null || Notice.Option.Subject == undefined || Notice.Option.Subject == "" || Notice.Option.Subject.lenght == 0) {

            $.notify("You Must Select The Options");
        }
        else {
            $scope.Notice.CourseName = Notice.Option.Subject.CourseTitle;
            $scope.Notice.CourseCode = Notice.Option.Subject.CourseCode;
        }

        if (Notice.Option.Class == null || Notice.Option.Class == undefined || Notice.Option.Class == "" || Notice.Option.Class.lenght == 0) {

            $.notify("You Must Select The Options");
        }
        else {
            $scope.Notice.Batch = Notice.Option.Class.Batch;
            $scope.Notice.Semester = Notice.Option.Class.Semester;
        }

        if (Notice.Option.Bilding == null || Notice.Option.Bilding == undefined || Notice.Option.Bilding == "" || Notice.Option.Bilding.lenght == 0) {

            $.notify("You Must Select The Options");
        }
        else {
            $scope.Notice.Dept = Notice.Option.Bilding.Dept;
        }
        
        if (Notice.NoticeTitle == null || Notice.NoticeTitle == undefined || Notice.NoticeTitle == "" || Notice.NoticeBody == null || Notice.NoticeBody == undefined || Notice.NoticeBody == "")
        {
            
            $.notify("You Must Write in 240 into Chatrektar !");
        }
        else {

            $http.post("/NoticeBoard/Notice", Notice).then(function (status) {
                $.notify("The Notice Send Success, Congratulation !");
            })
        }

    };


})



//for Notice Controller for View
var myAppForNoticeView = angular.module("noticeBoardView", []);
myAppForNoticeView.controller("noticeControllerView", function ($scope, $http) {
    $scope.messageNotice = "Hi Angular !";

    $http.get('/NoticeBoard/ViewBatchResult').then(function (result) {
        //alert(JSON.stringify(result.data));
        $scope.batchNoticeView = result.data;
    });


    $scope.myNoticeId = function (noticeId) {
        $scope.MyNotice = {
            id: ""
        };
        $scope.MyNotice.id = noticeId;


        $http.post("/NoticeBoard/NoticeDeteles", $scope.MyNotice).then(function (status) {

          //  alert(JSON.stringify(status.data));

            $scope.noticePage = status.data;
           // $scope.noticetast = 'hi Angular test';
          
        })


    }

})




