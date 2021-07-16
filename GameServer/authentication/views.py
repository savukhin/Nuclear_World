from django.shortcuts import render
from django.http import HttpResponse, JsonResponse
from django.contrib.auth import authenticate, login, logout
from django.contrib.auth.decorators import login_required
from authentication.forms import FormReg
from authentication.models import CustomUser
from django.contrib.auth.models import User
from django.views.decorators.csrf import csrf_exempt
from django.middleware.csrf import get_token

# Create your views here.

@csrf_exempt
def signIn(request):
    print("REQUEST")
    print("Took request", request, request.POST)
    if request.method == 'POST':
        user = authenticate(username=request.POST['username'], password=request.POST['password'])
        if user is not None:
            login(request, user)
            csrf_token = get_token(request)
            csrf_token_html = '<input type="hidden" name="csrfmiddlewaretoken" value="{}" />'.format(csrf_token)
            return JsonRespon({"csrfToken" : csrf_token}, status=200)

        return JsonResponse({'errors':[{"subject": "global", "text" : "Username or password is invalid"}], "csrfToken" : "None"}, status=401)
        #return HttpResponse('{"errors":[{"subject":"Global","text":"Username or password is invalid"}],"csrfToken":"None"}', status=401)
    return JsonResponse({'errors' : [{'global' : 'Must be POST request'}], "csrfToken" : "None"}, status=401)


@csrf_exempt
def signUp(request):
    response = 'Must be POST request'
    if request.method == 'POST':
        formUser = FormReg(request.POST)
        if formUser.is_valid():
            formUser.save()
            NewCustomUser = CustomUser(user=formUser.instance, pvpgn_user=newPvPGNProfile)
            NewCustomUser.save()
            user = authenticate(username=request.POST['username'], password=request.POST['password1'])
            login(request, user)
            return HttpResponse("Success", status=200)

        response = "Errors: " + formUser

    return HttpResponse(respone, status=401)


@login_required()
def signOut(request):
    logout(request)
    return HttpResponse("Success", status=200)
