<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassEval.aspx.cs" Inherits="FEWeb.Evaluation.ClassEval" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>课堂评价</title>
    <link href="../css/reset.css" rel="stylesheet" />
    <link href="../css/layout.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <script type="text/x-jquery-tmpl" id="item_TeaImmedEval">
        <tr>
            <td style="padding-left: 20px; text-align: left;">${Eva_Task.Name}</td>
            {{if Eva_Task.Status=="0"}}
            <td><span class="invest">未发布</span></td>
            {{else Eva_Task.Status=="2" && (DateTimeConvert(Eva_Task.StartTime,'yyyy-MM-dd',true) > getNowFormatDate())}}
            <td><span class="color_gray">未开始</span></td>
            {{else Eva_Task.Status=="2" && (DateTimeConvert(Eva_Task.EndTime,'yyyy-MM-dd',true) >= getNowFormatDate())}}
            <td><span class="checking">调查中</span></td>
            {{else DateTimeConvert(Eva_Task.EndTime,'yyyy-MM-dd',true) < getNowFormatDate() }}
            <td><span class="checked">已结束</span></td>
            {{/if}}
                        
            <td>${Eva_Task.IsScore=="0"?"是":"否"}</td>


            {{if Eva_Task.StartTime!=null && Eva_Task.StartTime!='1800-01-01'}}
            <td>${DateTimeConvert(Eva_Task.StartTime,'yyyy-MM-dd',true)}</td>
            {{else}}
            <td></td>
            {{else}}
            {{/if}}



            {{if Eva_Task.EndTime!=null && Eva_Task.EndTime!='1800-01-01'}}
            <td>${DateTimeConvert(Eva_Task.EndTime,'yyyy-MM-dd',true)}</td>
            {{else}}
            <td></td>
            {{else}}
            {{/if}}



            <td>${DateTimeConvert(Eva_Task.CreateTime,'yyyy-MM-dd',true)}</td>


            {{if Eva_Task.IsPublish==0}}
            {{if Eva_Task.IsSued==0}}
            <td><a href="javascript:EvalView(${Eva_Task.Id})" style="color: #eba714" onmouseover="$(this).css('text-decoration','underline')">${answer_count}</td>
            {{else Eva_Task.IsSued==1}}
              <td><a href="javascript:EvalView2(${Eva_Task.Id})" style="color: #eba714" onmouseover="$(this).css('text-decoration','underline')">${answer_count}</td>
            {{/if}}

             {{else}}
               <td>暂无</td>
            {{/if}}
          
            {{if Eva_Task.IsScore==0}}
            <td>${ave_score}</a></td>
            {{else}}
               <td>不计分</a></td>
            {{/if}}
         
                                  
            <td class="operate_wrap">{{if Eva_Task.Status=="0" ||(Eva_Task.Status=="2" && (DateTimeConvert(Eva_Task.StartTime,'yyyy-MM-dd',true) > getNowFormatDate()))}}
            <div class="operate" onclick="edit(${Eva_Task.Id},0)">
                <i class="iconfont color_purple">&#xe628;</i>
                <span class="operate_none bg_purple">编辑
                </span>
            </div>
                <div class="operate" onclick="view(${Eva_Task.Id})">
                    <i class="iconfont color_purple">&#xe60b;</i>
                    <span class="operate_none bg_purple">查看
                    </span>
                </div>
                {{if Eva_Task.Status=="0"}}
                <div class="operate" onclick="ReleaseTask(${Eva_Task.Id},'${Eva_Task.Name}')">
                    <i class="iconfont color_purple">&#xe62e;</i>
                    <span class="operate_none bg_purple">发布
                    </span>
                </div>
                {{else}}
                    <div class="operate">
                        <i class="iconfont color_gray">&#xe62e;</i>
                        <span class="operate_none bg_gray">发布
                        </span>
                    </div>
                {{else}}
                {{/if}}
               
                <div class="operate" onclick="copy(${Eva_Task.Id})">
                    <i class="iconfont color_purple">&#xe61e;</i>
                    <span class="operate_none bg_purple">复制
                    </span>
                </div>
                 <div class="operate">
                    <i class="iconfont color_gray">&#xe609;</i>
                    <span class="operate_none bg_gray">扫码
                    </span>
                </div>
                <div class="operate">
                    <i class="iconfont color_gray">&#xe742;</i>
                    <span class="operate_none bg_gray">统计
                    </span>
                </div>
                <div class="operate" onclick="del(${Eva_Task.Id})">
                    <i class="iconfont color_purple">&#xe61b;</i>
                    <span class="operate_none bg_purple">删除
                    </span>
                </div>
                {{else Eva_Task.Status=="1"}}
            <div class="operate">
                <i class="iconfont color_gray">&#xe628;</i>
                <span class="operate_none bg_gray">编辑
                </span>
            </div>
                <div class="operate" onclick="view(${Eva_Task.Id})">
                    <i class="iconfont color_purple">&#xe60b;</i>
                    <span class="operate_none bg_purple">查看
                    </span>
                </div>
                <div class="operate">
                    <i class="iconfont color_gray">&#xe62e;</i>
                    <span class="operate_none bg_gray">发布
                    </span>
                </div>
                <div class="operate" onclick="copy(${Eva_Task.Id})">
                    <i class="iconfont color_purple">&#xe61e;</i>
                    <span class="operate_none bg_purple">复制
                    </span>
                </div>
                <div class="operate" onclick="QRcode(${Eva_Task.Id},'${Eva_Task.StartTime}','${Eva_Task.EndTime}')">
                    <i class="iconfont color_purple">&#xe609;</i>
                    <span class="operate_none bg_purple">扫码
                    </span>
                </div>
                <div class="operate" onclick="statistics(${Eva_Task.Id})">
                    <i class="iconfont color_purple">&#xe742;</i>
                    <span class="operate_none bg_purple">统计
                    </span>
                </div>
                <div class="operate">
                    <i class="iconfont color_gray">&#xe61b;</i>
                    <span class="operate_none bg_gray">删除
                    </span>
                </div>
                {{else Eva_Task.Status=="2"}}
            <div class="operate">
                <i class="iconfont color_gray">&#xe628;</i>
                <span class="operate_none bg_gray">编辑
                </span>
            </div>
                <div class="operate" onclick="view(${Eva_Task.Id})">
                    <i class="iconfont color_purple">&#xe60b;</i>
                    <span class="operate_none bg_purple">查看
                    </span>
                </div>
                <div class="operate" id="ReleaseTask">
                    <i class="iconfont color_gray">&#xe62e;</i>
                    <span class="operate_none bg_gray">发布
                    </span>
                </div>
                <div class="operate" onclick="copy(${Eva_Task.Id})">
                    <i class="iconfont color_purple">&#xe61e;</i>
                    <span class="operate_none bg_purple">复制
                    </span>
                </div>
                <div class="operate" onclick="QRcode(${Eva_Task.Id},'${Eva_Task.StartTime}','${Eva_Task.EndTime}')">
                    <i class="iconfont color_purple">&#xe609;</i>
                    <span class="operate_none bg_purple">扫码
                    </span>
                </div>
                <div class="operate" onclick="statistics(${Eva_Task.Id})">
                    <i class="iconfont color_purple">&#xe742;</i>
                    <span class="operate_none bg_purple">统计
                    </span>
                </div>
                <div class="operate">
                    <i class="iconfont color_gray">&#xe61b;</i>
                    <span class="operate_none bg_gray">删除
                    </span>
                </div>
                {{/if}}
                
            </td>
        </tr>
    </script>
</head>
<body>
    <div id="top"></div>
    <div class="center" id="centerwrap">
        <div class="wrap clearfix">
             <div class="search_toobar clearfix">
                <div class="fl">
                    <label for="">学年学期:</label>

                    <select class="select" style="width: 198px;" id="section">
                        <option value="">全部</option>
                    </select>
                </div>
                  <div class="fl ml20">
                    <label for="">状态:</label>

                    <select class="select" style="width: 198px;" id="status">
                        <option value="">全部</option>
                    </select>
                </div>
                 <div class="fl ml20">
                    <input type="text" name="key" id="key" placeholder="请输入关键字" value="" class="text fl" style="width: 130px;">
                    <a class="search fl" href="javascript:search();"><i class="iconfont">&#xe600;</i></a>
                </div>
                 <div class="fr">
                     <input type="button" name="name" value="创建评价任务" class="btn"  onclick="NewEval();" />
                 </div>
            </div>
             <div class="table">
                <table>
                    <thead>
                         <tr>
                            <th style="padding-left: 20px; text-align: left;">评价名称  	
                            </th>
                            <th>状态
                            </th>
                            <th>是否记分</th>
                            <th>开始时间
                            </th>
                            <th>结束时间
                            </th>
                            <th>创建时间
                            </th>
                            <th>评价人数
                            </th>
                            <th>平均分
                            </th>
                            <th width="300px">操作
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tb_TeaImmedEval">
                      <%--  <tr>
                            <td width="300px" style="text-align:left;">
                                <div class="textoverflow" style="padding:0px 20px;">
                                    课程普通调查问卷模版			            		       	   	          	   		    	              		 
                                </div>
                            </td>
                           <td>
                               <span class="checking">调查中</span>
                           </td>
                            <td>是</td>
                            <td>2016-10-20</td>
                            <td>2016-10-22</td>
                            <td>2016-10-22</td>
                            <td class="operate_wrap">
                                <div class="operate">
                                    <i class="iconfont color_gray">&#xe628;</i>
                                    <span class="operate_none bg_gray">编辑</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_purple">&#xe60b;</i>
                                    <span class="operate_none bg_purple">查看</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_purple">&#xe609;</i>
                                    <span class="operate_none bg_purple">扫码</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_purple">&#xe61e;</i>
                                    <span class="operate_none bg_purple">复制</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_gray">&#xe742;</i>
                                    <span class="operate_none bg_gray">统计</span>
                                </div>
                                    <div class="operate">
                                    <i class="iconfont color_gray">&#xe61b;</i>
                                    <span class="operate_none bg_gray">删除</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="300px" style="text-align:left;">
                                <div class="textoverflow" style="padding:0px 20px;">
                                    课程普通调查问卷模版			            		       	   	          	   		    	              		 
                                </div>
                            </td>
                           <td>
                               <span class="checked">已结束</span>
                           </td>
                            <td>是</td>
                            <td>2016-10-20</td>
                            <td>2016-10-22</td>
                            <td>2016-10-22</td>
                            <td class="operate_wrap">
                                <div class="operate">
                                    <i class="iconfont color_gray">&#xe628;</i>
                                    <span class="operate_none bg_gray">编辑</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_purple">&#xe60b;</i>
                                    <span class="operate_none bg_purple">查看</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_gray">&#xe609;</i>
                                    <span class="operate_none bg_gray">扫码</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_purple">&#xe61e;</i>
                                    <span class="operate_none bg_purple">复制</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_purple">&#xe742;</i>
                                    <span class="operate_none bg_purple">统计</span>
                                </div>
                                    <div class="operate">
                                    <i class="iconfont color_gray">&#xe61b;</i>
                                    <span class="operate_none bg_gray">删除</span>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td width="300px" style="text-align:left;">
                                <div class="textoverflow" style="padding:0px 20px;">
                                    课程普通调查问卷模版			            		       	   	          	   		    	              		 
                                </div>
                            </td>
                           <td>
                               <span class="">未发布</span>
                           </td>
                            <td>是</td>
                            <td>2016-10-20</td>
                            <td>2016-10-22</td>
                            <td>2016-10-22</td>
                            <td class="operate_wrap">
                                <div class="operate">
                                    <i class="iconfont color_purple">&#xe628;</i>
                                    <span class="operate_none bg_purple">编辑</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_purple">&#xe60b;</i>
                                    <span class="operate_none bg_purple">查看</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_gray">&#xe609;</i>
                                    <span class="operate_none bg_gray">扫码</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_purple">&#xe61e;</i>
                                    <span class="operate_none bg_purple">复制</span>
                                </div>
                                <div class="operate">
                                    <i class="iconfont color_gray">&#xe742;</i>
                                    <span class="operate_none bg_gray">统计</span>
                                </div>
                                    <div class="operate">
                                    <i class="iconfont color_purple">&#xe61b;</i>
                                    <span class="operate_none bg_purple">删除</span>
                                </div>
                            </td>

                        </tr>--%>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <footer id="footer"></footer>
    <script src="../Scripts/Common.js"></script>
     <script src="../Scripts/public.js"></script>
    
    <script src="../Scripts/linq.min.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <link href="../Scripts/kkPage/Css.css" rel="stylesheet" />
    <script src="../Scripts/kkPage/jquery.kkPages.js"></script>
    <script>
        $(function () {
            $('#top').load('/header.html');
            $('#footer').load('/footer.html');
            tableSlide();
        })
    </script>

      <script type="text/javascript">
          var retDataCache = null;
          $(function () {


              initdata('');
              //学期搜索
              $("#section").change(function () {
                  initdata('');
              })
              $("#status").change(function () {
                  initdata('');
              })


          })


          //统计
          function statistics(Task_Id) {
              location.href = "MyStatiscal.aspx?page=0&Task_Id=" + Task_Id + '&Id=' + getQueryString("Id") + '&Iid=' + getQueryString("Iid");
          }

          //编辑评价
          function edit(id, type) {
              location.href = 'EditAddEvalTable.aspx?Id='+getQueryString("Id")+'&Iid='+getQueryString("Iid")+'&idd='+id+ "&Type=" + type;
          }

          //预览评价
          function view(id) {
              //window.open('/MyEvaluation/TeaImmedEval_Detail.aspx?table_Id=' + id);\
              OpenIFrameWindow('预览评价', 'TableView.aspx?table_Id=' + id, '1100px', '700px');
          }

          //复制
          function copy(id) {
              layer.confirm('确定要复制？', {
                  btn: ['确定', '取消'],
                  title: '操作'
              }, function () {

                  //先查询该任务下的表的详情
                  $.ajax({
                      url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                      type: "post",
                      async: false,
                      dataType: "json",
                      data: { Func: "Get_Eva_Common_ById_Mobile", "Id": id },
                      success: function (json) {
                          retData = json.result.retData;
                          var CreateUID = GetLoginUser().UniqueNo;
                          var EditUID = GetLoginUser().UniqueNo;
                          ////添加任务试卷
                          var submit_data = new Object();
                          submit_data.func = "Add_Eva_Common";
                          submit_data.Name = retData.Eva_Task.Name;//任务名称
                          submit_data.IsScore = retData.Eva_Task.IsScore;//是否计分
                          submit_data.Remarks = retData.Eva_Task.Remarks;//备注
                          submit_data.CreateUID = CreateUID;
                          submit_data.EditUID = EditUID;
                          submit_data.List = JSON.stringify(retData.eva_detail_list);//答卷
                          submit_data.StartTime = retData.Eva_Task.StartTime;//开始时间
                          submit_data.EndTime = retData.Eva_Task.EndTime;//结束时间
                          submit_data.TeacherUID = GetLoginUser().UniqueNo;
                          submit_data.CourseRoom_Id = 1;
                          submit_data.FullScore = 100;
                          submit_data.Range = '';
                          submit_data.Status = 0;
                          submit_data.StudySection_Id = 1;
                          submit_data.Type = 0;
                          $.ajax({
                              url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                              type: "post",
                              async: false,
                              dataType: "json",
                              data: submit_data,//组合input标签
                              success: function (json) {
                                  if (json.result.errMsg == "success") {
                                      initdata('');
                                      layer.msg('复制成功!');
                                  }
                              },
                              error: function () {
                                  //接口错误时需要执行的
                              }
                          });
                          ////添加任务试卷
                      },
                      error: function () {
                          //接口错误时需要执行的
                      }
                  });

              });
          }

          function del(id) {
              layer.confirm('您确定要删除？', {
                  btn: ['确定', '取消'], //按钮
                  title: '操作'
              }, function () {
                  $.ajax({
                      url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                      type: "post",
                      async: false,
                      dataType: "json",
                      data: { Func: "Delete_Eva_Common", "Id": id },
                      success: function (json) {
                          if (json.result.errMsg == "success") {
                              initdata('');
                              layer.msg('删除成功', { icon: 1 });
                          }
                      },
                      error: function () {
                          //接口错误时需要执行的
                      }
                  });

              });

          }

          function NewEval() {
              window.location.href = 'AddEvalTable.aspx?Id=' + getQueryString('Id') + '&Iid=' + getQueryString('Iid')//即时评价
          }
          function eval_check(_this, _flg) {
              $(_flg).parents('ul').find("li").removeClass('selected');
              $(_flg).addClass("selected")
              initdata(_this);
          }

          //点击搜索
          function search() {
              initdata('');
          }
          var user = GetLoginUser();

          if (user.Sys_Role == '超级管理员') {
              $('#tongji').css("display", 'block');
              $('#system_setting').css("display", 'block');

          }



          //初始化表格列表
          function initdata(_this) {
              var TeacherUID = GetLoginUser().UniqueNo;
              $.ajax({
                  url: HanderServiceUrl + "/Eva_Manage/Eva_ManageHandler.ashx",
                  type: "post",
                  async: false,
                  dataType: "json",
                  data: { Func: "Get_Eva_Common", TeacherUID: TeacherUID, Type: 0 },
                  success: function (json) {
                      $("#tb_TeaImmedEval").empty();
                      $(".Pagination").remove();
                      if (json.result.retData != "没有数据") {
                          //清理数据

                          //排序，最新的数据排列在最上边
                          retDataCache = Enumerable.From(json.result.retData).OrderByDescending('$.Id').ToArray();//按Id进行升序排列

                          //学期下拉框
                          //var section = $("#section").val();
                          //if (section != "") {
                          //    retDataCache = Enumerable.From(retDataCache).Where("item=>item.StudySection_Id==" + section + "").ToArray();
                          //}
                          console.log(retDataCache)

                         /* var StartTime = $("#StartTime").val();
                          var EndTime = $("#EndTime").val();
                          if (StartTime != "") {
                              retDataCache = Enumerable.From(retDataCache).Where(function (i) {
                                  //各个条件

                                  var flg = false;
                                  if (i.Eva_Task.StartTime != null) {
                                      var StartTime_z = DateTimeConvert(i.Eva_Task.StartTime, 'yyyy-MM-dd', true);
                                      if (StartTime_z <= StartTime) {
                                          flg = true;
                                      }
                                  }


                                  if (flg) { return i }
                              }).ToArray();
                          }
                          if (EndTime != "") {
                              retDataCache = Enumerable.From(retDataCache).Where(function (i) {
                                  //各个条件

                                  var flg = false;
                                  if (i.Eva_Task.EndTime != null) {
                                      var EndTime_z = DateTimeConvert(i.Eva_Task.EndTime, 'yyyy-MM-dd', true);
                                      if (EndTime_z >= EndTime) {
                                          flg = true;
                                      }
                                  }

                                  if (flg) { return i }
                              }).ToArray();
                          }

                          //状态下拉框
                          var status = $("#status").val();
                          if (_this != "") {
                              status = _this;
                          }
                          if (status == "0") {//未发布
                              retDataCache = Enumerable.From(retDataCache).Where("item=>item.Eva_Task.Status==" + status + "").ToArray();
                          }
                          else if (status == "1")//调查中
                          {
                              retDataCache = Enumerable.From(retDataCache).Where("item=>item.Eva_Task.Status==2").ToArray();//状态为2 表示已发布
                              retDataCache = Enumerable.From(retDataCache).Where(function (i) {
                                  //各个条件
                                  var flg = false;
                                  var EndTime_z = DateTimeConvert(i.Eva_Task.EndTime, 'yyyy-MM-dd', true);
                                  var StartTime_z = DateTimeConvert(i.Eva_Task.StartTime, 'yyyy-MM-dd', true);
                                  if (EndTime_z >= getNowFormatDate() && StartTime_z <= getNowFormatDate()) {
                                      flg = true;
                                  }

                                  if (flg) { return i }
                              }).ToArray();
                          }
                          else if (status == "3")//未开始
                          {
                              retDataCache = Enumerable.From(retDataCache).Where("item=>item.Eva_Task.Status==2").ToArray();//状态为2 表示已发布
                              retDataCache = Enumerable.From(retDataCache).Where(function (i) {
                                  //各个条件
                                  var flg = false;
                                  var StartTime_z = DateTimeConvert(i.Eva_Task.StartTime, 'yyyy-MM-dd', true);
                                  if (StartTime_z > getNowFormatDate()) {
                                      flg = true;
                                  }

                                  if (flg) { return i }
                              }).ToArray();
                          }
                          else if (status == "2")//已结束
                          {
                              retDataCache = Enumerable.From(retDataCache).Where(function (i) {
                                  //各个条件
                                  var flg = false;
                                  if (i.Eva_Task.EndTime != null) {
                                      var EndTime_z = DateTimeConvert(i.Eva_Task.EndTime, 'yyyy-MM-dd', true);
                                      if (EndTime_z < getNowFormatDate()) {
                                          flg = true;
                                      }
                                  }



                                  if (flg) { return i }
                              }).ToArray();

                          }

                          //关键字搜索
                          var key = $("#key").val();
                          if (key != "") {
                              retDataCache = Enumerable.From(retDataCache).Where(function (i) {
                                  //各个条件

                                  var flg = false;

                                  if (i.Eva_Task.Name.indexOf(key) > -1) {
                                      flg = true;
                                  }


                                  if (flg) { return i }
                              }).ToArray();
                              //retDataCache = Enumerable.From(retDataCache).Where("item=>item.Eva_Task.Name.indexOf('" + key + "')>-1").ToArray();
                          }*/
                          //为空显示暂无数据
                          if (retDataCache.length <= 0) {
                              nomessage("#tb_TeaImmedEval");//无数据页
                              return;
                          }
                          //console.log(retDataCache);
                          //追加数据
                          $("#item_TeaImmedEval").tmpl(retDataCache).appendTo("#tb_TeaImmedEval");
                          //分页
                          $('.table').kkPages({
                              PagesClass: 'tbody tr', //需要分页的元素
                              PagesMth: 10, //每页显示个数
                              PagesNavMth: 4 //显示导航个数
                          });
                          tableSlide();
                      }
                      else {
                          nomessage("#tb_TeaImmedEval");//无数据页
                          return;
                      }
                  },
                  error: function () {
                      //接口错误时需要执行的
                  }
              });
          }

          function getNowFormatDate() {
              var date = new Date();
              var seperator1 = "-";
              var seperator2 = ":";
              var month = date.getMonth() + 1;
              var strDate = date.getDate();
              if (month >= 1 && month <= 9) {
                  month = "0" + month;
              }
              if (strDate >= 0 && strDate <= 9) {
                  strDate = "0" + strDate;
              }
              var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
              return currentdate;
          }

          //分配
          function ReleaseTask(id, Name) {
              OpenIFrameWindow('发布任务', 'ReleaseTask.aspx?Id=' + id + '&type=0&name=' + Name + '', '600px', '400px')
          }

          //分配
          function QRcode(id, StartTime, EndTime) {
              OpenIFrameWindow('二维码', 'QRcode.aspx?url=' + MobileUrl + 'Mobile/onlinetest.html?id=' + id + '&StartTime=' + StartTime + '&EndTime=' + EndTime, '300px', '300px');
          }

          function EvalView(taskId) {
              OpenIFrameWindow('查看', 'EvalView.aspx?taskId=' + taskId, '1000px', '700px')
          }

          function EvalView2(taskId) {
              OpenIFrameWindow('查看', 'EvalView_NoSued.aspx?taskId=' + taskId, '1000px', '700px')
          }
    </script>
</body>
</html>