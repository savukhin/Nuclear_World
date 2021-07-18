from django.shortcuts import render
from django.http import HttpResponse, JsonResponse
from django.contrib.auth import authenticate, login, logout
from django.contrib.auth.decorators import login_required
from authentication.forms import FormReg
from authentication.models import CustomUser
from django.contrib.auth.models import User
from django.views.decorators.csrf import csrf_exempt
from django.middleware.csrf import get_token
import json

# Create your views here.

@csrf_exempt
def signIn(request):
    print(request.POST)
    if request.method == 'POST':
        username = request.POST['username']
        user = authenticate(username=username, password=request.POST['password'])
        if user is not None:
            login(request, user)
            csrf_token = get_token(request)
            csrf_token_html = '<input type="hidden" name="csrfmiddlewaretoken" value="{}" />'.format(csrf_token)
            if (username != 'admin'):
                with open("authentication/connectedUsers.json", "r") as jsonFile:
                    jsonObject = json.load(jsonFile)
                    jsonObject['NewConnectedUsers'].append(username)
                with open("authentication/connectedUsers.json", "w") as jsonFile:
                    json.dump(jsonObject, jsonFile)
            return JsonResponse({"csrfToken" : csrf_token, "sessionID" : request.session.session_key}, status=200)

        return JsonResponse({'errors':[{"subject": "global", "text" : "Username or password is invalid"}], "csrfToken" : "None", "sessionID" : "None"}, status=401)
    return JsonResponse({'errors' : [{'global' : 'Must be POST request'}], "csrfToken" : "None", "sessionID" : "None"}, status=401)


def signUp(request):
    response = 'Must be POST request'
    if request.method == 'POST':
        formUser = FormReg(request.POST)
        if formUser.is_valid():
            formUser.save()
            NewCustomUser = CustomUser(user=formUser.instance)
            NewCustomUser.save()
            return HttpResponse("Success", status=200)
        return render(request, template_name='signUp.html', context={'form': formUser})

    return render(request, "signUp.html")


@login_required()
def signOut(request):
    logout(request)
    return HttpResponse("Success", status=200)
